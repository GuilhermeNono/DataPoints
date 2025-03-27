using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.Batch.Validation.Queries.FindLastestValidationAvailable;

namespace DataPoints.Infrastructure.Persistence.Main.Batch.Validation;

public class BatchValidationRepository : CrudRepository<BatchValidationEntity, Guid>, IBatchValidationRepository
{
    public BatchValidationRepository(MainContext context) : base(context)
    {
    }


    public Task<BatchValidationEntity?> FindLatestValidationAvailable()
    {
        var query = new FindLatestValidationAvailableQuery(new FindLatestValidationAvailableFilter())
            .OrderBy(x => x.BeginValidation, Sort.Desc);

        return QuerySingle(query);
    }
}