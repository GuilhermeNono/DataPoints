using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;

namespace DataPoints.Domain.Entities.Main;

[Table("btc_checkpoints")]
public class BatchCheckpointEntity : Entity<long>
{
    public Guid IdBlock { get; set; }
    public Guid IdBatch { get; set; }
    public bool IsValid { get; set; } = true;
}