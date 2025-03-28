using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindByBlockId;

public class FindByBlockIdQuery(FindByBlockIdFilter filter) : CustomQuery<FindByBlockIdFilter, WalletTransactionEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Wlt_Transactions");
        Add($"    WHERE IdBlock = {Param(x => x.BlockId)} ");
    }
}