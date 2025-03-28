using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindByBlockId;

public record FindByBlockIdFilter(Guid BlockId) : IFilter
{
    
}