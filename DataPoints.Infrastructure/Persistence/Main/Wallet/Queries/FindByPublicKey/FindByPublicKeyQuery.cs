using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindByPublicKey;

public class FindByPublicKeyQuery(FindByPublicKeyFilter filter) : CustomQuery<FindByPublicKeyFilter, WalletEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM core.wlt_wallets ");
        Add($"   WHERE publickey = {Param(x => x.PublicKey)} ");
        Add($"     AND isactive = {Param(x => x.IsActive)} ");
        Add($"     AND isblocked = {Param(x => x.IsBlocked)} ");
    }
}