using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Entities.Main;

namespace DataPoints.Domain.Repositories.Main;

public interface IWalletTransactionRepository : ICrudRepository<WalletTransactionEntity, Guid>
{
    Task<decimal> FindAmountByWallet(Guid walletId);
    Task<IEnumerable<WalletTransactionEntity>> FindByIds(IEnumerable<Guid> requestTransactions);
}
