using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.Permission.Queries.FindByName;
using DataPoints.Infrastructure.Persistence.Main.Permission.Queries.FindByUser;
using Microsoft.AspNetCore.Identity;

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

    public Task<IEnumerable<PermissionEntity>> FindByUser(Guid userId)
    {
        var query = new FindByUserQuery(new FindByUserFilter(userId));

        return Task.FromResult(Query(query));
    }

    public Task<IdentityResult> CreateAsync(PermissionEntity role, CancellationToken cancellationToken)
    {
        return Task.FromResult(IdentityResult.Success);
    }

    public Task<IdentityResult> UpdateAsync(PermissionEntity role, CancellationToken cancellationToken)
    {
        return Task.FromResult(IdentityResult.Success);
    }

    public Task<IdentityResult> DeleteAsync(PermissionEntity role, CancellationToken cancellationToken)
    {
        return Task.FromResult(IdentityResult.Success);
    }

    public Task<string> GetRoleIdAsync(PermissionEntity role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Id.ToString());
    }

    public Task<string?> GetRoleNameAsync(PermissionEntity role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Name)!;
    }

    public Task SetRoleNameAsync(PermissionEntity role, string? roleName, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedRoleNameAsync(PermissionEntity role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Name)!;
    }

    public Task SetNormalizedRoleNameAsync(PermissionEntity role, string? normalizedName, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<PermissionEntity?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        return FindById(int.Parse(roleId));
    }

    public Task<PermissionEntity?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        return FindByName(normalizedRoleName);
    }
}
