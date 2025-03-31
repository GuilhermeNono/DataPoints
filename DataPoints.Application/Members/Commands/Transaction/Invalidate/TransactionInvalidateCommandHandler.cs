using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Domain.Repositories.Main;
using MediatR;

namespace DataPoints.Application.Members.Commands.Transaction.Invalidate;

public class TransactionInvalidateCommandHandler : ICommandHandler<TransactionInvalidateCommand>
{
    
    private readonly IWalletTransactionRepository _walletTransactionRepository;

    public TransactionInvalidateCommandHandler(IWalletTransactionRepository walletTransactionRepository)
    {
        _walletTransactionRepository = walletTransactionRepository;
    }

    public async Task Handle(TransactionInvalidateCommand request, CancellationToken cancellationToken)
    {
        var transactions = await _walletTransactionRepository.FindByBlockId(request.BlockId);

        foreach (var transaction in transactions)
        {
            transaction.IsBlocked = true;
            await _walletTransactionRepository.Update(transaction, request.LoggedPerson.Name, cancellationToken);
        }
    }
}