using System.ComponentModel.DataAnnotations.Schema;
using DataPoints.Domain.Database.Entity.Interfaces;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Helpers;

namespace DataPoints.Domain.Database.Entity;

public abstract class AuditableStatefulEntity<TId> : AuditableEntity<TId>, IStateable
{
    public bool IsActive { get; protected set; } = true;
    public virtual void ChangeActiveStatus(bool newStatus = default) => IsActive = newStatus;
}
