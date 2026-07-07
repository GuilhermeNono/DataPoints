using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;
using DataPoints.Domain.Database.Entity.Interfaces;

namespace DataPoints.Domain.Entities.Audit;

[Table("aud_userjourney")]
public class UserJourneyEntity : Entity<long>, IEntityLog
{
    public string CorrelationId { get; set; } = string.Empty;
    public Guid? IdUser { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string? Resource { get; set; }

    [Column(TypeName = "jsonb")]
    public string? PayloadJson { get; set; }

    public string? Ip { get; set; }
    public string? UserAgent { get; set; }
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    public static UserJourneyEntity For(string correlationId, string eventType, Guid? idUser = null,
        string? resource = null, object? payload = null) => new()
    {
        CorrelationId = correlationId,
        EventType = eventType,
        IdUser = idUser,
        Resource = resource,
        PayloadJson = payload is null ? null : System.Text.Json.JsonSerializer.Serialize(payload),
    };
}
