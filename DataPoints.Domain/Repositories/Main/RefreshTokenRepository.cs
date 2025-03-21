using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Entities.Main;

namespace DataPoints.Domain.Repositories.Main;

public interface IRefreshTokenRepository : ICrudRepository<RefreshTokenEntity, long>
{
    Task<RefreshTokenEntity?> FindByRefreshToken(string refreshToken);
}