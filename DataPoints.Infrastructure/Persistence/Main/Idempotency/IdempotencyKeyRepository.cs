using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;

namespace DataPoints.Infrastructure.Persistence.Main.Idempotency;

public class IdempotencyKeyRepository : CrudRepository<IdempotencyKeyEntity, Guid>, IIdempotencyKeyRepository
{
    public IdempotencyKeyRepository(MainContext context) : base(context)
    {
    }
}
