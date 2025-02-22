using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Permission.Queries.FindByUser;

public record FindByUserFilter(Guid Id) : IFilter
{
    
}