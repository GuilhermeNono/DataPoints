using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.Profile.Queries.FindByRole;

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
}
