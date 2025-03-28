using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Crosscutting.Exceptions.Http.Internal;
using DataPoints.Crosscutting.Exceptions.Http.NotFound;
using DataPoints.Domain.Entities.Audit;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Audit;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Commands.Wallets.Balance.Update;

public class WalletBalanceUpdateCommandHandler : ICommandHandler<WalletBalanceUpdateCommand, decimal>
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

    public async Task<decimal> Handle(WalletBalanceUpdateCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _wallet.FindById(request.WalletId) 
                     ?? throw new WalletNotFoundException(request.WalletId);

        var newBalance = await _walletTransaction.FindAmountByWallet(request.WalletId);

        if (newBalance >= 0) return newBalance;
        
        wallet.IsBlocked = true;
            
        throw new ProhibitedBalanceAmountException(newBalance);
    }
}