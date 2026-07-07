using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindByHash;

public class FindByHashQuery(FindByHashFilter filter) : CustomQuery<FindByHashFilter, WalletEntity>(filter)
{
    protected override void Prepare()
    {
        Add("    SELECT * ");
        Add("      FROM core.wlt_wallets ");
        Add($"    WHERE hash = {Param(x => x.Hash)} ");
        Add($"      AND isactive = {Param(x => x.IsActive)} ");
        Add($"      AND isblocked = {Param(x => x.IsBlocked)} ");
    }
}