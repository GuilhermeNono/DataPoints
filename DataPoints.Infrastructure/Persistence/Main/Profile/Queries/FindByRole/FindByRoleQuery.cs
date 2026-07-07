using DataPoints.Domain.Database.Queries.Base;
using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Profile.Queries.FindByRole;

public class FindByRoleQuery(FindByRoleFilter filter) : CustomQuery<FindByRoleFilter, ProfileEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT pmp.* ");
        Add("     FROM core.prm_profiles pmp ");
        Add("    INNER JOIN core.prm_permissions pms On pms.id = pmp.idpermission ");
        Add($"   WHERE pms.name = {Param(x => x.Role)} ");
        Add($"     AND pmp.iduser = {Param(x => x.IdUser)} ");
        Add($"     AND pms.isblocked = {Param(x => x.IsBlocked)} ");
    }
}
