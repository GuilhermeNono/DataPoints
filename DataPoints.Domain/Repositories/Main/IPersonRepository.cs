using DataPoints.Domain.CustomQuery;
using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Entities.Main;

namespace DataPoints.Domain.Repositories.Main;

public interface IPersonRepository : ICrudRepository<PersonEntity, Guid>
{
    Task<PersonEntity?> FindByDocument(string documentNormalized);
    Task<PersonEntity?> FindByNormalizedDocument(string document);
    Task<UserInfoQueryResponse?> FindResponseByDocument(string documentNormalized);
}
