using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.Profile.Queries.FindByRole;
using Microsoft.AspNetCore.Identity;

namespace DataPoints.Infrastructure.Persistence.Main.Profile;

public class ProfileRepository : CrudRepository<ProfileEntity, long>, IProfileRepository
{
    public ProfileRepository(MainContext context) : base(context)
    {
    }

    public Task<ProfileEntity?> FindByRole(Guid idUser, string role)
    {
        var query = new FindByRoleQuery(new FindByRoleFilter(idUser, role));

        return QuerySingle(query);
    }

    public Task<IdentityResult> CreateAsync(ProfileEntity role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(ProfileEntity role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(ProfileEntity role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetRoleIdAsync(ProfileEntity role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetRoleNameAsync(ProfileEntity role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetRoleNameAsync(ProfileEntity role, string? roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetNormalizedRoleNameAsync(ProfileEntity role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedRoleNameAsync(ProfileEntity role, string? normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ProfileEntity?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ProfileEntity?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
