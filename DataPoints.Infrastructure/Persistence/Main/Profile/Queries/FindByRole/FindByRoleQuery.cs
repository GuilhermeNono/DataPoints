using DataPoints.Domain.Database.Queries.Base;
using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Profile.Queries.FindByRole;

public class FindByRoleQuery(FindByRoleFilter filter) : CustomQuery<FindByRoleFilter, ProfileEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT pmp.* ");
        Add("     FROM Prm_Profiles pmp ");
        Add("    INNER JOIN Prm_Permissions pms On pms.Id = pmp.IdPermission ");
        Add($"   WHERE pms.Name = {Param(x => x.Role)} ");
        Add($"     AND pmp.IdUser = {Param(x => x.IdUser)} ");
        Add($"     AND pms.IsBlocked = {Param(x => x.IsBlocked)} ");
    }
}
