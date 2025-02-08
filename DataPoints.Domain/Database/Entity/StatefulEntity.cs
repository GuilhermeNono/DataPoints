using DataPoints.Domain.Database.Entity.Interfaces;

namespace DataPoints.Domain.Database.Entity;

public abstract class StatefulEntity<TId> : IEntity<TId>, IStateable 
{
    public TId? Id { get; init; }

    public bool IsActive { get; protected set; } = true;
    public virtual void ChangeActiveStatus(bool newStatus = default) => IsActive = newStatus;
}