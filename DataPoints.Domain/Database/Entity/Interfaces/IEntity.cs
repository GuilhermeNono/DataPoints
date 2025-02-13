namespace DataPoints.Domain.Database.Entity.Interfaces;

public interface IEntity<TType> 
{
    public new TType Id { get; init; }
}
