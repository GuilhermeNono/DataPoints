using DataPoints.Domain.Database.Context;
using DataPoints.Domain.Database.Entity.Interfaces;

namespace DataPoints.Domain.Database.Repository;

public interface IViewRepository<TEntity> : IEFContext where TEntity : class, IEntityView, new()
{
    Task<IAsyncEnumerable<TEntity>> FindAsync();
}
