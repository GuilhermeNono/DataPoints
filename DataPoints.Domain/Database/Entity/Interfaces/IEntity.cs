namespace DataPoints.Domain.Database.Entity.Interfaces;

public interface IEntity<TType>
{
    public TType? Id { get; init; }
}