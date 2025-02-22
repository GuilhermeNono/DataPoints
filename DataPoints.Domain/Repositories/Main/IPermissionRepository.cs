using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Entities.Main;
using Microsoft.AspNetCore.Identity;

namespace DataPoints.Domain.Repositories.Main;

public interface IPermissionRepository : ICrudRepository<PermissionEntity, int>, IRoleStore<PermissionEntity>
{
    Task<PermissionEntity?> FindByName(string role);
    Task<IEnumerable<PermissionEntity>> FindByUser(Guid userId);
}
