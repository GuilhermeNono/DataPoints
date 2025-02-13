using DataPoints.Domain.Database.Entity.Interfaces;

namespace DataPoints.Domain.Database.Entity;

public abstract class Entity<TId> : IEntity<TId>
{
    public TId? Id { get; init; }
}
