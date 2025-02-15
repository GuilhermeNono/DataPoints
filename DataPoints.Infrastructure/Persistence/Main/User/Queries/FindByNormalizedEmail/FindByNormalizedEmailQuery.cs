using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.User.Queries.FindByNormalizedEmail;

public class FindByNormalizedEmailQuery(FindByNormalizedEmailFilter filter) : CustomQuery<FindByNormalizedEmailFilter, UserEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Ath_Users ");
        Add($"    WHERE NormalizedEmail = {Param(x => x.Login)} ");
    }
}
