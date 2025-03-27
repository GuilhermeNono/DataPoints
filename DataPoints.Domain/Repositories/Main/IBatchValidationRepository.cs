using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Entities.Main;

namespace DataPoints.Domain.Repositories.Main;

public interface IBatchValidationRepository : ICrudRepository<BatchValidationEntity, Guid>
{
    Task<BatchValidationEntity?> FindLatestValidationAvailable();
}