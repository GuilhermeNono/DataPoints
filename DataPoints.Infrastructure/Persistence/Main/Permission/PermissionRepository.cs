using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.Permission.Queries.FindByName;

namespace DataPoints.Infrastructure.Persistence.Main.Permission;

public class PermissionRepository : CrudRepository<PermissionEntity, int>, IPermissionRepository
{
    public PermissionRepository(MainContext context) : base(context)
    {
    }

    public Task<PermissionEntity?> FindByName(string role)
    {
        var query = new FindByNameQuery(new FindByNameFilter(role));

        return QuerySingle(query);
    }
}
