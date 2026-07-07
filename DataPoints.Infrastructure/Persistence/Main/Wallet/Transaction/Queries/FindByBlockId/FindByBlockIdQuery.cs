using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindByBlockId;

public class FindByBlockIdQuery(FindByBlockIdFilter filter) : CustomQuery<FindByBlockIdFilter, WalletTransactionEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM wlt_transactions");
        Add($"    WHERE idblock = {Param(x => x.BlockId)} ");
    }
}