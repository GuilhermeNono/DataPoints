using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindByIdBlock;

public class FindByIdBlockQuery(FindByIdBlockFilter filter) : CustomQuery<FindByIdBlockFilter, WalletTransactionEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM wlt_transactions ");
        Add($"    WHERE idblock = {Param(x => x.BlockId)} ");
    }
}