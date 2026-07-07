using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;
using DataPoints.Domain.Enums.Entities;

namespace DataPoints.Domain.Entities.Main;

[Table("btc_validations")]
public class BatchValidationEntity : Entity<Guid>
{
    public DateTime BeginValidation { get; set; } = DateTime.UtcNow;
    public DateTime? EndValidation { get; set; }
    public BatchStateType IdBatchStatus { get; set; } = BatchStateType.Pending;
    public int BlockInvalidated { get; set; }
    public int BlockProcessed { get; set; }
}