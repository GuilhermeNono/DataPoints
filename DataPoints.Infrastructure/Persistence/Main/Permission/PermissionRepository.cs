using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;

namespace DataPoints.Infrastructure.Persistence.Main.Permission;

public class PermissionRepository : CrudRepository<PermissionEntity, int>, IPermissionRepository
{
    public PermissionRepository(MainContext context) : base(context)
    {
    }
}
