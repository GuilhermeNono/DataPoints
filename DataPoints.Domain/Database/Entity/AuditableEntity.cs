using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity.Interfaces;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Helpers;

namespace DataPoints.Domain.Database.Entity;

public abstract class AuditableEntity<TId> : IEntity<TId>, IAudit
{
    public TId? Id { get; init; }

    public string Operation { get; protected set; } = OperationHelper.Create;
    
    [NotMapped]
    public OperationEnum OperationEnum
    {
        get => Enum.Parse<OperationEnum>(Operation, true);
        set => Operation = value.ToString();
    }
    public string AuditUser { get; set; } = UserHelper.System;
    public DateTime AuditDate { get; set; }
}
