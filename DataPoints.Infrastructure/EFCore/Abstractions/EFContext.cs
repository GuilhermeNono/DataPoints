using DataPoints.Domain.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace DataPoints.Infrastructure.EFCore.Abstractions;

public abstract class EfContext<TEntity> : IEFContext, IAsyncDisposable where TEntity : class
{
    protected DbContext Context { get; set; }
    protected DbSet<TEntity> Model { get; set; }

    protected EfContext(DbContext context)
    {
        Context = context;
        Model = context.Set<TEntity>();
        Context.ChangeTracker.AutoDetectChangesEnabled = false;
        Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await Context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
