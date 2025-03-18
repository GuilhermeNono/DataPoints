using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindByUser;

public record FindByUserFilter(Guid UserId) : IFilter
{
    public bool IsActive { get; set; } = true;
    public bool IsBlocked { get; set; } = false;
}