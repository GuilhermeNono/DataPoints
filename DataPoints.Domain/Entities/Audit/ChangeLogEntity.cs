using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using DataPoints.Domain.Database.Entity;
using DataPoints.Domain.Database.Entity.Interfaces;
using DataPoints.Domain.Enums;

namespace DataPoints.Domain.Entities.Audit;

[Table("aud_changelog")]
public class ChangeLogEntity : AuditableEntity<long>, IEntityLog
{
    public string EntityName { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;

    public string TargetOperation { get; set; } = string.Empty;

    [Column(TypeName = "jsonb")]
    public string? SnapshotJson { get; set; }

    public static ChangeLogEntity For(string entityName, object entityId, InternalOperation targetOperation,
        object snapshot) => new()
    {
        EntityName = entityName,
        EntityId = entityId.ToString() ?? string.Empty,
        TargetOperation = targetOperation.ToString(),
        SnapshotJson = JsonSerializer.Serialize(snapshot),
    };
}
