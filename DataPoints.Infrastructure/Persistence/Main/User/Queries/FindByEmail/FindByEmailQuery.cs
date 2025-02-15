using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.User.Queries.FindByEmail;

public class FindByEmailQuery(FindByEmailFilter filter) : CustomQuery<FindByEmailFilter, UserEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Ath_Users ");
        Add($"    WHERE Email = {Param(x => x.Login)} ");
    }
}
