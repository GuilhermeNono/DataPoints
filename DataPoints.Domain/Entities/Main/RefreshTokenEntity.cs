using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;

namespace DataPoints.Domain.Entities.Main;

[Table("ath_refreshtokens")]
public class RefreshTokenEntity : AuditableEntity<long>
{
    [Column("token")]
    public string TokenHash { get; set; } = string.Empty;

    public Guid IdUser { get; set; }
    public DateTime DateExpired { get; set; } = DateTime.UtcNow.AddMinutes(10);
    public DateTime? RevokedAt { get; set; }
    public long? ReplacedByTokenId { get; set; }

    public bool IsExpired => DateExpired.CompareTo(DateTime.UtcNow) <= 0;
    public bool IsRevoked => RevokedAt is not null;
}