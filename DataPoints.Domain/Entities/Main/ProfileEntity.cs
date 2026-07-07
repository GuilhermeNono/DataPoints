using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;

namespace DataPoints.Domain.Entities.Main;

[Table("prm_profiles")]
public class ProfileEntity : AuditableEntity<long>
{
    public Guid IdUser { get; set; }
    public int IdPermission { get; set; }
}
