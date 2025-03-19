using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindByIds;

public record FindByIdsFilter(IEnumerable<Guid> Ids) : IFilter
{
    public string IdJoined => string.Join(",", Ids);
}