namespace DataPoints.Domain.Database.Entity.Interfaces;

public interface IEntityLog
{
}

public interface IEntityLog<TId> : IEntity<TId>
{
}