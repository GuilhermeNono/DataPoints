using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Profile.Queries.FindByRole;

public record FindByRoleFilter(Guid IdUser, string Role) : IFilter
{
    public bool IsBlocked => false;
}
