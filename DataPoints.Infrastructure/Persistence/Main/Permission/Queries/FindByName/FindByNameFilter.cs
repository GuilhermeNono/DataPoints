using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Permission.Queries.FindByName;

public record FindByNameFilter(string Role) : IFilter
{
}
