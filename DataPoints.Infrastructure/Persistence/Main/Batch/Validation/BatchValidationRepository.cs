using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;

namespace DataPoints.Infrastructure.Persistence.Main.Batch.Validation;

public class BatchValidationRepository : CrudRepository<BatchValidationEntity, Guid>, IBatchValidationRepository
{
    public BatchValidationRepository(MainContext context) : base(context)
    {
    }
}