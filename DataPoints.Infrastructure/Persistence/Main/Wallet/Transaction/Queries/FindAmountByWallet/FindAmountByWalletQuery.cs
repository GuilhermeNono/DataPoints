using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindAmountByWallet;

public class FindAmountByWalletQuery(FindAmountByWalletFilter filter) : CustomQuery<FindAmountByWalletFilter, decimal>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT COALESCE(SUM(amount), 0) as Value");
        Add("     FROM wlt_transactions ");
        Add($"   WHERE idwalletto = {Param(x => x.WalletId)} ");
    }
}