using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Entities.Main;

namespace DataPoints.Domain.Repositories.Main;

public interface IIdempotencyKeyRepository : ICrudRepository<IdempotencyKeyEntity, Guid>
{
}
