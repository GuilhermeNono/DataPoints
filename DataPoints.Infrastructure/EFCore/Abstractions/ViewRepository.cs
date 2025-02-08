using DataPoints.Domain.Database.Entity.Interfaces;
using DataPoints.Domain.Database.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataPoints.Infrastructure.EFCore.Abstractions;

public abstract class ViewRepository<TEntity> : CustomQueryRepository<TEntity>, IViewRepository<TEntity>
    where TEntity : class, IEntityView, new()
{
    protected ViewRepository(DbContext context) : base(context)
    {
    }

    public Task<IAsyncEnumerable<TEntity>> FindAsync()
    {
        return Task.FromResult(Model.AsNoTracking().AsAsyncEnumerable());
    }
}
