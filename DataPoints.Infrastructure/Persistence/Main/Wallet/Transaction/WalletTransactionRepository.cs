using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindAmountByWallet;
using DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindByIds;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction;

public class WalletTransactionRepository : CrudRepository<WalletTransactionEntity, Guid>, IWalletTransactionRepository
{
    public WalletTransactionRepository(MainContext context) : base(context)
    {
    }

    public Task<decimal> FindAmountByWallet(Guid walletId)
    {
        var query = new FindAmountByWalletQuery(new FindAmountByWalletFilter(walletId));

        return QuerySingle(query);
    }

    public Task<IEnumerable<WalletTransactionEntity>>  FindByIds(IEnumerable<Guid> transactionsId)
    {
        var query = new FindByIdsQuery(new FindByIdsFilter(transactionsId.ToArray()));
        
        return Task.FromResult(Query(query));
    }
}
