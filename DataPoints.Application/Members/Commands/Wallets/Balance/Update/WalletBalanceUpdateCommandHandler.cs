using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Crosscutting.Exceptions.Http.NotFound;
using DataPoints.Domain.Entities.Audit;
using DataPoints.Domain.Repositories.Audit;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Commands.Wallets.Balance.Update;

public class WalletBalanceUpdateCommandHandler : ICommandHandler<WalletBalanceUpdateCommand>
{
    private readonly IWalletRepository _wallet;
    private readonly IWalletLogRepository _walletLog;
    private readonly IWalletTransactionRepository _walletTransaction;

    public WalletBalanceUpdateCommandHandler(IWalletRepository wallet, IWalletTransactionRepository walletTransaction, IWalletLogRepository walletLog)
    {
        _wallet = wallet;
        _walletTransaction = walletTransaction;
        _walletLog = walletLog;
    }

    public async Task Handle(WalletBalanceUpdateCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _wallet.FindById(request.WalletId) 
                     ?? throw new WalletNotFoundException(request.WalletId);

        var newBalance = await _walletTransaction.FindAmountByWallet(request.WalletId);

        if (newBalance < 0)
            throw new Exception("Houve um problema durante a atualização da carteira.");
        
        wallet.Balance = newBalance;
        
        await _wallet.Update(wallet, request.LoggedPerson.Name, cancellationToken);
        await _walletLog.Update(new WalletLogEntity(wallet), request.LoggedPerson.Name, cancellationToken);
    }
}