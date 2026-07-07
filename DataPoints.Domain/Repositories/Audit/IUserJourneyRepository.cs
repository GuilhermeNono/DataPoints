using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Entities.Audit;

namespace DataPoints.Domain.Repositories.Audit;

public interface IUserJourneyRepository : IAuditRepository<UserJourneyEntity, long>
{
}
