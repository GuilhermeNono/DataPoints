using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity.Interfaces;
using DataPoints.Domain.Entities.Main;

namespace DataPoints.Domain.Entities.Audit;

[Table("Prm_Profiles")]
public class ProfileLogEntity : ProfileEntity, IEntityLog
{
    public long IdProfile { get; set; }

    public ProfileLogEntity()
    {
    }

    public ProfileLogEntity(ProfileEntity entity)
    {
        IdProfile = entity.Id;
        IdUser = entity.IdUser;
        IdPermission = entity.IdPermission;
    }
}
