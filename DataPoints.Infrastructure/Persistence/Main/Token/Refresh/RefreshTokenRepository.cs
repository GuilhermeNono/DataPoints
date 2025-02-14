using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;

namespace DataPoints.Infrastructure.Persistence.Main.Token.Refresh;

public class RefreshTokenRepository : CrudRepository<RefreshTokenEntity, long>, IRefreshTokenRepository
{
    public RefreshTokenRepository(MainContext context) : base(context)
    {
    }
}