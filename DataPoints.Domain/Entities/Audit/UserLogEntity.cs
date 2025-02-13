using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity.Interfaces;
using DataPoints.Domain.Entities.Main;

namespace DataPoints.Domain.Entities.Audit;

[Table("Ath_User")]
public class UserLogEntity : UserEntity, IEntityLog
{
    public new long Id { get; init; }
    public Guid IdUser { get; set; }

    public UserLogEntity()
    {
    }

    public UserLogEntity(UserEntity entity)
    {
        IdUser = entity.Id;
        Email = entity.Email;
        IsEmailConfirmed = entity.IsEmailConfirmed;
        PasswordHash = entity.PasswordHash;
        Salt = entity.Salt;
    }
}
