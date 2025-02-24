using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Profile.Queries.FindRolesByUser;

public class FindRolesByUserQuery(FindRolesByUserFilter filter) : CustomQuery<FindRolesByUserFilter, PermissionEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT pm.* ");
        Add("     FROM Prm_Profiles p");
        Add("    INNER JOIN Prm_Permissions pm On pm.Id = p.IdPermission ");
        Add($"   WHERE p.IdUser = {Param(x => x.IdUser)} ");
    }
}