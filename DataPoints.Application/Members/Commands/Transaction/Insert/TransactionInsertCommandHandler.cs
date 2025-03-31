using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Application.Members.Commands.Block.Insert;
using DataPoints.Application.Members.Commands.Wallets.Balance.Update;
using DataPoints.Contract.Transaction.Insert;
using DataPoints.Crosscutting.Exceptions.Http.BadRequest;
using DataPoints.Crosscutting.Exceptions.Http.Conflict;
using DataPoints.Crosscutting.Exceptions.Http.Internal;
using DataPoints.Crosscutting.Exceptions.Http.NotFound;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Wallet;
using DataPoints.Domain.Database.Queries.Base;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Helpers;
using DataPoints.Domain.Objects;
using DataPoints.Domain.Objects.Security;
using DataPoints.Domain.Repositories.Main;
using MediatR;

namespace DataPoints.Application.Members.Commands.Transaction.Insert;

public class TransactionInsertCommandHandler : ICommandHandler<TransactionInsertCommand, TransactionInsertResponse>
{
    private readonly ISender _sender;
    private readonly IWalletRepository _walletRepository;
    private readonly IWalletTransactionRepository _walletTransactionRepository;

    private decimal _senderWalletAmount = decimal.Zero;
    private decimal _receiverWalletAmount = decimal.Zero;

    public TransactionInsertCommandHandler(ISender sender, IWalletRepository walletRepository,
        IWalletTransactionRepository walletTransactionRepository)
    {
        _sender = sender;
        _walletRepository = walletRepository;
        _walletTransactionRepository = walletTransactionRepository;
    }

    public async Task<TransactionInsertResponse> Handle(TransactionInsertCommand request,
        CancellationToken cancellationToken)
    {
        if (request.LoggedPerson.Id is null)
            throw new LoggedPersonNotFoundException();
        
        if(!SecurityHelper.IsValidPrivateKey(request.Signature))
            throw new SignatureIsInvalidException();
        
        var receiver = await _walletRepository.FindByHash(request.ReceiverWallet)
                       ?? throw new WalletHashNotFoundException(request.ReceiverWallet);

        var sender = await _walletRepository.FindByUser(request.LoggedPerson.Id.GetValueOrDefault())
                     ?? throw new WalletUserNotFoundException(request.LoggedPerson.Id.GetValueOrDefault());

        if (!SecurityHelper.IsKeyPairValid(sender.PublicKey, request.Signature))
            throw new SignatureIsInvalidException();
        
        if (receiver.Id == sender.Id)
            throw new TransactionForYourselfException();

        if (sender.IsBlocked || !receiver.IsActive)
            throw new SenderWalletUnavailableException();

        if (receiver.IsBlocked || !receiver.IsActive)
            throw new ReceiverWalletIsUnavailableException();

        var walletsId = new Dictionary<WalletType, WalletEntity>
        {
            { WalletType.Receiver, receiver },
            { WalletType.Sender, sender }
        };

        await UpdateWallets(walletsId, request.LoggedPerson, cancellationToken);

        if (_senderWalletAmount < request.Amount)
            throw new InsufficientBalanceException();

        var senderTransaction = CreateTransaction(request.Amount, sender.Id, receiver.Id, TransactionType.Credited);
        var receiverTransaction = CreateTransaction(request.Amount, receiver.Id, sender.Id, TransactionType.Debited);

        await _walletTransactionRepository.Add(senderTransaction, request.LoggedPerson.Name, cancellationToken);
        await _walletTransactionRepository.Add(receiverTransaction, request.LoggedPerson.Name, cancellationToken);

        await UpdateWallets(walletsId, request.LoggedPerson, cancellationToken);

        var groupKeys = new SecurityGroupKey(sender.PublicKey, request.Signature);

        var blockHash =
            await _sender.Send(
                new BlockInsertCommand([senderTransaction.Id, receiverTransaction.Id], groupKeys, request.LoggedPerson),
                cancellationToken);

        return new TransactionInsertResponse(blockHash, DateTime.Now);
    }

    private async Task UpdateWallets(Dictionary<WalletType, WalletEntity> wallets, LoggedPerson loggedPerson,
        CancellationToken cancellationToken)
    {
        foreach (var (type, wallet) in wallets)
        {
            if (type is WalletType.Receiver)
            {
                _receiverWalletAmount = await _sender.Send(new WalletBalanceUpdateCommand(wallet.Id, loggedPerson),
                    cancellationToken);
                continue;
            }

            _senderWalletAmount =
                await _sender.Send(new WalletBalanceUpdateCommand(wallet.Id, loggedPerson), cancellationToken);
        }
    }

    private static WalletTransactionEntity CreateTransaction(decimal amount, Guid senderId, Guid receiverId,
        TransactionType type) => new()
    {
        Amount = type is TransactionType.Credited ? amount : amount * -1,
        IsCredit = type is TransactionType.Credited,
        IdWalletFrom = type is TransactionType.Credited ? senderId : receiverId,
        IdWalletTo = type is TransactionType.Credited ? receiverId : senderId,
    };
}