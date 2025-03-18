using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Wallet.Queries.FindByPublicKey;

public record FindByPublicKeyFilter(string PublicKey) : IFilter
{
    public bool IsActive { get; set; } = true;
    public bool IsBlocked { get; set; } = false;
}