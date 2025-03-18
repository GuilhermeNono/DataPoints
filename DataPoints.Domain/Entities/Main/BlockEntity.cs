using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;

namespace DataPoints.Domain.Entities.Main;

[Table("Blc_Block")]
public class BlockEntity : Entity<Guid>
{
    public string Hash { get; set; } = string.Empty;
    public string PreviousHash { get; set; } = string.Empty;
    public DateTime DateInclusion { get; set; }
}