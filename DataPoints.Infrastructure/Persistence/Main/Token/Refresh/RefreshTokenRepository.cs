using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Helpers;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.Token.Refresh.Queries;

namespace DataPoints.Infrastructure.Persistence.Main.Token.Refresh;

public class RefreshTokenRepository : CrudRepository<RefreshTokenEntity, long>, IRefreshTokenRepository
{
    public RefreshTokenRepository(MainContext context) : base(context)
    {
    }

    public Task<RefreshTokenEntity?> FindByRefreshToken(string refreshToken)
    {
        var tokenHash = SecurityHelper.CreateSha256Key(refreshToken).Hash;

        var query = new FindByRefreshTokenQuery(new FindByRefreshTokenFilter(tokenHash));

        return QuerySingle(query);
    }
}