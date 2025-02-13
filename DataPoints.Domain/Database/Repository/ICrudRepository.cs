using DataPoints.Domain.Database.Entity.Interfaces;

namespace DataPoints.Domain.Database.Repository;

public interface ICrudRepository<TEntity, in TId> : IReadRepository<TEntity, TId> 
    where TEntity : class, IEntity<TId>, new()
{
    Task<int> Delete(TEntity entity, CancellationToken cancellationToken);
    Task<int> Delete(TId id, CancellationToken cancellationToken);
    Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> Update(TEntity entity, string userWhoUpdated, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> Update(IList<TEntity> entities, string userWhoUpdated, CancellationToken cancellationToken);
    Task<TEntity> Add(TEntity entity, string userWhoAdded, CancellationToken cancellationToken);
}
