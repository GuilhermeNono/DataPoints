using DataPoints.Domain.Database.Entity;

namespace DataPoints.Domain.Entities.Main;

public class BatchValidationEntity : Entity<Guid>
{
    public DateTime BeginValidation { get; set; } = DateTime.Now;
    public DateTime? EndValidation { get; set; }
    public int IdBatchStatus { get; set; }
    public int BlockInvalidated { get; set; }
    public int BlockProcessed { get; set; }
}