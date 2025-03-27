using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindAmountByWallet;

public class FindAmountByWalletQuery(FindAmountByWalletFilter filter) : CustomQuery<FindAmountByWalletFilter, decimal>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT IIF(SUM(Amount) is null, 0, SUM(Amount)) as Value");
        Add("     FROM Wlt_Transactions "); 
        Add($"   WHERE IdWalletTo = {Param(x => x.WalletId)} "); 
    }
}