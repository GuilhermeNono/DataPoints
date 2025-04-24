using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindBalanceByUser;

public class FindBalanceByUserQuery(FindBalanceByUserFilter filter)
    : CustomQuery<FindBalanceByUserFilter, decimal>(filter)
{
    protected override void Prepare()
    {
        Add(
            $"   SELECT COALESCE(SUM(t.Amount), 0) as Value FROM Wlt_Transactions t Inner Join Wlt_Wallets w On w.Id = t.IdWalletTo WHERE w.IdUser = {Param(x => x.IdPerson)}");
    }
}