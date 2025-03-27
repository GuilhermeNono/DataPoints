using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.Batch.Checkpoint.Queries.FindByBatch;
using Microsoft.EntityFrameworkCore;

namespace DataPoints.Infrastructure.Persistence.Main.Batch.Checkpoint;

public class BatchCheckpointRepository : CrudRepository<BatchCheckpointEntity, long>, IBatchCheckpointRepository
{
    public BatchCheckpointRepository(MainContext context) : base(context)
    {
    }

    public Task<IEnumerable<BatchCheckpointEntity>> FindByBatch(Guid validationId, bool showInvalidatedOnes = true)
    {
        var query = new FindByBatchQuery(new FindByBatchFilter(validationId));
        
        return Task.FromResult(Query(query));
    }
}