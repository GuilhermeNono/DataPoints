using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;

namespace DataPoints.Domain.Entities.Main;

[Table("Ath_RefreshTokens")]
public class RefreshTokenEntity : AuditableEntity<long>
{
    public string Token { get; set; } = string.Empty;
    public Guid IdUser { get; set; }
    public DateTime DateExpired { get; set; } = DateTime.UtcNow.AddMinutes(10);

    public bool IsExpired => DateExpired.CompareTo(DateTime.UtcNow) <= 0;
}