using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindByHash;

public record FindByHashFilter(string Hash) : IFilter
{
    public bool IsActive { get; set; } = true;
    public bool IsBlocked { get; set; } = false;
}