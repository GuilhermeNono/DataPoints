using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Entities.Audit;

namespace DataPoints.Domain.Repositories.Audit;

public interface IPermissionLogRepository : IAuditRepository<PermissionLogEntity, long>
{
}
