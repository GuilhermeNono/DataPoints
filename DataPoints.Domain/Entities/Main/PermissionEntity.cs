using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity;
using DataPoints.Domain.Enums;

namespace DataPoints.Domain.Entities.Main;

[Table("Prm_Permissions")]
public class PermissionEntity : AuditableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
    public InternalPermission InternalPermission => (InternalPermission)Id;
}
