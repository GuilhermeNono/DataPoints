using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;

namespace DataPoints.Domain.Entities.Main;

[Table("Ath_Users")]
public class UserEntity : AuditableEntity<Guid>
{
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
}
