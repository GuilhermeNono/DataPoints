using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;

namespace DataPoints.Domain.Entities.Main;

[Table("ath_users")]
public class UserEntity : AuditableStatefulEntity<Guid>
{
    public string Email { get; set; } = string.Empty;
    public string NormalizedEmail { get; set; } = string.Empty;
    public string SecurityStamp { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public DateTimeOffset? LockoutEnd { get; set; }
    public int AccessFailedCount { get; set; }
    public bool LockoutEnabled { get; set; } = true;
}
