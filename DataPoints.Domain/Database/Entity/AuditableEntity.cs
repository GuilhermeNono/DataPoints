using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity.Interfaces;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Helpers;

namespace DataPoints.Domain.Database.Entity;

public abstract class AuditableEntity<TId> : Entity<TId>, IAudit
{
    public string Operation { get; protected set; } = OperationHelper.Create;
    
    [NotMapped]
    public InternalOperation InternalOperation
    {
        get => Enum.Parse<InternalOperation>(Operation, true);
        set => Operation = value.ToString();
    }
    public string LastChangeBy { get; set; } = UserHelper.System;
    public DateTime LastChangeAt { get; set; }
}
