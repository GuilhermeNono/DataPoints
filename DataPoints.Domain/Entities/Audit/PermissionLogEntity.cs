using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity.Interfaces;
using DataPoints.Domain.Entities.Main;

namespace DataPoints.Domain.Entities.Audit;

[Table("Prm_Permissions")]
public class PermissionLogEntity : PermissionEntity, IEntityLog
{
    public new long Id { get; init; }
    public long IdPermission { get; set; }

    public PermissionLogEntity()
    {
    }

    public PermissionLogEntity(PermissionEntity entity)
    {
        IdPermission = entity.Id;
        Name = entity.Name;
        IsBlocked = entity.IsBlocked;
    }
}
