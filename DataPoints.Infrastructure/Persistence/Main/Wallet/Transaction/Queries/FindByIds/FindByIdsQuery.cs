using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindByIds;

public class FindByIdsQuery(FindByIdsFilter filter) : CustomQuery<FindByIdsFilter, WalletTransactionEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Wlt_Transactions ");
        Add($"    WHERE Id in ({Param(x => x.IdJoined)}) ");
    }
}