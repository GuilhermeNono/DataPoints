using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity.Interfaces;
using DataPoints.Domain.Entities.Main;

namespace DataPoints.Domain.Entities.Audit;

[Table("Ath_User")]
public class PersonLogEntity : PersonEntity, IEntityLog
{
    public new long Id { get; init; }
    public Guid IdPerson { get; set; }

    public PersonLogEntity()
    {
    }

    public PersonLogEntity(PersonEntity entity)
    {
        IdPerson = entity.Id;
        FirstName = entity.FirstName;
        LastName = entity.LastName;
        Avatar = entity.Avatar;
        BirthDate = entity.BirthDate;
        IsActive = entity.IsActive;
        DocumentNumber = entity.DocumentNumber;
        DocumentType = entity.DocumentType;
        DateInclusion = entity.DateInclusion;
    }
}
