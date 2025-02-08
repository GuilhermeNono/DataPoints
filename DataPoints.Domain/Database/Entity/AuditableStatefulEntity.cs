using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity.Interfaces;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Helpers;

namespace DataPoints.Domain.Database.Entity;

public abstract class AuditableStatefulEntity<TId> : IEntity<TId>, IAudit, IStateable
{
    public TId? Id { get; init; }
    public string Operation { get; private set; } = OperationHelper.Create;
    
    [NotMapped]
    public OperationEnum OperationEnum
    {
        get => Enum.Parse<OperationEnum>(Operation, true);
        set => Operation = value.ToString();
    }
    public string AuditUser { get; set; } = UserHelper.System;
    public DateTime AuditDate { get; set; }
    public bool IsActive { get; protected set; } = true;
    public virtual void ChangeActiveStatus(bool newStatus = default) => IsActive = newStatus;
}
