using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindBalanceByUser;

public class FindBalanceByUserQuery(FindBalanceByUserFilter filter)
    : CustomQuery<FindBalanceByUserFilter, decimal>(filter)
{
    protected override void Prepare()
    {
        Add(
            $"   SELECT COALESCE(SUM(t.amount), 0) as Value FROM core.wlt_transactions t Inner Join core.wlt_wallets w On w.id = t.idwalletto WHERE w.iduser = {Param(x => x.IdPerson)}");
    }
}