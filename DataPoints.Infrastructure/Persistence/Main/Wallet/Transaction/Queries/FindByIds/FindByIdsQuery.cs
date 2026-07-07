using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindByIds;

public class FindByIdsQuery(FindByIdsFilter filter) : CustomQuery<FindByIdsFilter, WalletTransactionEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM wlt_transactions ");
        Add($"    WHERE id in ({Param(x => x.Ids)}) ");
    }
}