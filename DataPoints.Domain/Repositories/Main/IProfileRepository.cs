using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Entities.Main;
using Microsoft.AspNetCore.Identity;

namespace DataPoints.Domain.Repositories.Main;

public interface IProfileRepository : ICrudRepository<ProfileEntity, long>
{
    Task<ProfileEntity?> FindByRole(Guid idUser, string role);
    Task<IEnumerable<PermissionEntity>> FindRolesByUser(Guid idUser);
}
