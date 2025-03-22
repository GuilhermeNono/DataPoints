using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction.Queries.FindByIdBlock;

public record FindByIdBlockFilter(Guid BlockId) : IFilter;