using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindByUser;

public class FindByUserQuery(FindByUserFilter filter) : CustomQuery<FindByUserFilter, WalletEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Wlt_Wallets ");
        Add($"   WHERE IdUser = {Param(x => x.UserId)} ");
        Add($"     AND IsActive = {Param(x => x.IsActive)} ");
        Add($"     AND IsBlocked = {Param(x => x.IsBlocked)} ");
    }
}