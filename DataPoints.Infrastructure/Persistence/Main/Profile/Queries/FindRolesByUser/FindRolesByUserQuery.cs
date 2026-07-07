using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Profile.Queries.FindRolesByUser;

public class FindRolesByUserQuery(FindRolesByUserFilter filter) : CustomQuery<FindRolesByUserFilter, PermissionEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT pm.* ");
        Add("     FROM core.prm_profiles p");
        Add("    INNER JOIN core.prm_permissions pm On pm.id = p.idpermission ");
        Add($"   WHERE p.iduser = {Param(x => x.IdUser)} ");
    }
}