using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;
using DataPoints.Domain.Enums.Entities;

namespace DataPoints.Domain.Entities.Main;

[Table("idm_idempotencykeys")]
public class IdempotencyKeyEntity : Entity<Guid>
{
    public string RequestHash { get; set; } = string.Empty;
    public IdempotencyStatus Status { get; set; } = IdempotencyStatus.Processing;
    public string? ResponseBody { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddHours(24);
}
