using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.User.Queries.FindByLogin;

public class FindByLoginQuery(FindByLoginFilter filter) : CustomQuery<FindByLoginFilter, UserEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Ath_Users ");
        Add($"    WHERE Email = {Param(x => x.Login)} ");
    }
}