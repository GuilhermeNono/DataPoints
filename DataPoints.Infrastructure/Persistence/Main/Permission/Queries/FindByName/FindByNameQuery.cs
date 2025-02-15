using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Permission.Queries.FindByName;

public class FindByNameQuery(FindByNameFilter filter) : CustomQuery<FindByNameFilter, PermissionEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Prm_Permissions ");
        Add($"   WHERE Name = {Param(x => x.Role)}  ");
    }
}
