using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindByPublicKey;

public class FindByPublicKeyQuery(FindByPublicKeyFilter filter) : CustomQuery<FindByPublicKeyFilter, WalletEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Wlt_Wallets ");
        Add($"   WHERE PublicKey = {Param(x => x.PublicKey)} ");
        Add($"     AND IsActive = {Param(x => x.IsActive)} ");
        Add($"     AND IsBlocked = {Param(x => x.IsBlocked)} ");
    }
}