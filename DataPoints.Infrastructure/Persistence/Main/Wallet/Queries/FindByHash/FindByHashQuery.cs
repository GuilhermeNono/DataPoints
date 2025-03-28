using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindByHash;

public class FindByHashQuery(FindByHashFilter filter) : CustomQuery<FindByHashFilter, WalletEntity>(filter)
{
    protected override void Prepare()
    {
        Add("    SELECT * ");
        Add("      FROM Wlt_Wallet ");
        Add($"    WHERE Hash = {Param(x => x.Hash)} ");
        Add($"      AND IsActive = {Param(x => x.IsActive)} ");
        Add($"      AND IsBlocked = {Param(x => x.IsBlocked)} ");
    }
}