using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindBalanceByUser;

public record FindBalanceByUserFilter(Guid IdPerson) : IFilter;