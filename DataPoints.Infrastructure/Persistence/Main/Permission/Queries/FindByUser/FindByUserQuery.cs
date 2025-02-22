using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Permission.Queries.FindByUser;

public class FindByUserQuery(FindByUserFilter filter) : CustomQuery<FindByUserFilter, PermissionEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT p.* ");
        Add("   FROM Prm_Permissions p ");
        Add("  INNER JOIN Prm_Profiles pp ON pp.IdPermission = p.Id ");
        Add($" WHERE pp.IdUser = {Param(x => x.Id)} ");
    }
}