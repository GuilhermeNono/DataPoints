using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity.Interfaces;
using DataPoints.Domain.Entities.Main;

namespace DataPoints.Domain.Entities.Audit;

[Table("Wlt_Wallets")]
public class WalletLogEntity : WalletEntity, IEntityLog
{
    public new long Id { get; init; }
    public Guid IdWallet { get; set; }

    public WalletLogEntity()
    {
    }

    public WalletLogEntity(WalletEntity entity)
    {
        IdWallet = entity.Id;
        IdUser = entity.IdUser;
        PublicKey = entity.PublicKey;
        Hash = entity.Hash;
        IsBlocked = entity.IsBlocked;
        IsActive = entity.IsActive;
    }
}
