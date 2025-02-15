using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Entities.Main;
using Microsoft.AspNetCore.Identity;

namespace DataPoints.Domain.Repositories.Main;

public interface IProfileRepository : ICrudRepository<ProfileEntity, long>, IRoleStore<ProfileEntity>
{
    Task<ProfileEntity?> FindByRole(Guid idUser, string role);
}
