using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Entities.Main;

namespace DataPoints.Domain.Repositories.Main;

public interface IBatchCheckpointRepository : ICrudRepository<BatchCheckpointEntity, long>
{
    Task<IEnumerable<BatchCheckpointEntity>> FindByBatch(Guid validationId, bool showInvalidatedOnes = true);
}