using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Application.Members.Commands.Transaction.Block.Insert;
using DataPoints.Application.Members.Commands.Wallets.Balance.Update;
using DataPoints.Contract.Transaction.Insert;
using DataPoints.Crosscutting.Exceptions.Http.Internal;
using DataPoints.Crosscutting.Exceptions.Http.NotFound;
using DataPoints.Domain.Database.Queries.Base;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Objects;
using DataPoints.Domain.Repositories.Main;
using MediatR;

namespace DataPoints.Application.Members.Commands.Transaction.Insert;

public class TransactionInsertCommandHandler : ICommandHandler<TransactionInsertCommand, TransactionInsertResponse>
{
    private readonly ISender _sender;
    private readonly IWalletRepository _walletRepository;
    private readonly IWalletTransactionRepository _walletTransactionRepository;

    public TransactionInsertCommandHandler(ISender sender, IWalletRepository walletRepository,
        IWalletTransactionRepository walletTransactionRepository)
    {
        _sender = sender;
        _walletRepository = walletRepository;
        _walletTransactionRepository = walletTransactionRepository;
    }

    public async Task<TransactionInsertResponse> Handle(TransactionInsertCommand request, CancellationToken cancellationToken)
    {
        if (request.LoggedPerson.Id is null)
            throw new LoggedPersonNotFoundException();

        var receiver = await _walletRepository.FindByPublicKey(request.ReceiverPublicKey)
                       ?? throw new WalletPublicKeyNotFoundException(request.ReceiverPublicKey);

        var sender = await _walletRepository.FindByUser(request.LoggedPerson.Id.GetValueOrDefault())
                     ?? throw new WalletUserNotFoundException(request.LoggedPerson.Id.GetValueOrDefault());
        
        var walletsId = new List<Guid>{receiver.Id, sender.Id};
        
        await UpdateWalletBalance(walletsId, request.LoggedPerson, cancellationToken);

        if (receiver.Id == sender.Id)
            throw new Exception();
        
        if(sender.Balance < request.Amount)
            throw new Exception();
        
        if(receiver.IsBlocked || !receiver.IsActive)
            throw new Exception();

        var senderTransaction = new WalletTransactionEntity
        {
            Amount = request.Amount,
            IsCredit = true,
            IdWalletFrom = sender.Id,
            IdWalletTo = receiver.Id,
        };

        var receiverTransaction = new WalletTransactionEntity
        {
            Amount = request.Amount * -1,
            IsCredit = false,
            IdWalletFrom = receiver.Id,
            IdWalletTo = sender.Id,
        };
        
        await _walletTransactionRepository.Add(senderTransaction, request.LoggedPerson.Name, cancellationToken);
        await _walletTransactionRepository.Add(receiverTransaction, request.LoggedPerson.Name, cancellationToken);

        await UpdateWalletBalance(walletsId, request.LoggedPerson, cancellationToken);

        var blockHash =
            await _sender.Send(
                new BlockInsertCommand([senderTransaction.Id, receiverTransaction.Id], request.LoggedPerson),
                cancellationToken);

        return new TransactionInsertResponse(blockHash, DateTime.Now);
    }

    private async Task UpdateWalletBalance(IList<Guid> walletsId, LoggedPerson loggedPerson, CancellationToken cancellationToken)
    {
        foreach (var walletId in walletsId)
            await _sender.Send(new WalletBalanceUpdateCommand(walletId, loggedPerson), cancellationToken);
    } 
}