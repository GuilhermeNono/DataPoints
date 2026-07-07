using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindByUser;

public class FindByUserQuery(FindByUserFilter filter) : CustomQuery<FindByUserFilter, WalletEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM core.wlt_wallets ");
        Add($"   WHERE iduser = {Param(x => x.UserId)} ");
        Add($"     AND isactive = {Param(x => x.IsActive)} ");
        Add($"     AND isblocked = {Param(x => x.IsBlocked)} ");
    }
}