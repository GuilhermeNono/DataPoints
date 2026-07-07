using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Permission.Queries.FindByUser;

public class FindByUserQuery(FindByUserFilter filter) : CustomQuery<FindByUserFilter, PermissionEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT p.* ");
        Add("   FROM core.prm_permissions p ");
        Add("  INNER JOIN core.prm_profiles pp ON pp.idpermission = p.id ");
        Add($" WHERE pp.iduser = {Param(x => x.Id)} ");
    }
}