using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Crosscutting.Exceptions.Http.Internal;
using DataPoints.Crosscutting.Exceptions.Http.NotFound;
using DataPoints.Domain.Entities.Audit;
using DataPoints.Domain.Entities.Main;
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
        {
            wallet.IsBlocked = true;
            
            await UpdateWallet(wallet, request.LoggedPerson.Name, cancellationToken);
            
            throw new ProhibitedBalanceAmountException(newBalance);
        }
        
        wallet.Balance = newBalance;

        await UpdateWallet(wallet, request.LoggedPerson.Name, cancellationToken);
    }

    private async Task UpdateWallet(WalletEntity wallet, string loggedPersonName, CancellationToken cancellationToken)
    {
        await _wallet.Update(wallet, loggedPersonName, cancellationToken);
        await _walletLog.Update(new WalletLogEntity(wallet), loggedPersonName, cancellationToken);
    }
}