using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Token.Refresh.Queries;

public class FindByRefreshTokenQuery(FindByRefreshTokenFilter filter) : CustomQuery<FindByRefreshTokenFilter, RefreshTokenEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Ath_RefreshTokens ");
        Add($"    WHERE Token = {Param(x => x.RefreshToken)} ");
    }
}