using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Block.Queries.FindNonValidated;

public record FindNonValidatedFilter(Guid ValidationId) : IFilter
{
    public bool IsValid => true;
}