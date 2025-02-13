using DataPoints.Crosscutting.Exceptions.Http.NotFound;
using DataPoints.Domain.Database.Entity.Interfaces;
using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DataPoints.Infrastructure.EFCore.Abstractions;

public abstract class CrudRepository<TEntity, TId>
    : ReadRepository<TEntity, TId>, ICrudRepository<TEntity, TId> where TEntity : class, IEntity<TId>, new()
{
    protected CrudRepository(DbContext context) : base(context)
    {
    }

    public virtual async Task<int> Delete(TEntity entity, CancellationToken cancellationToken)
    {
        Model.Remove(entity);

        int numberOfEntries = await Context.SaveChangesAsync(cancellationToken);

        Context.ChangeTracker.Clear();

        return numberOfEntries;
    }

    public virtual async Task<int> Delete(TId id, CancellationToken cancellationToken)
    {
        var entity = await FindById(id) ??
                     throw new EntityToDeleteNotFound(nameof(TEntity));

        return await Delete(entity, cancellationToken);
    }

    public virtual async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken)
    {
        Model.Update(entity);

        UpdateAuditOfEntity(entity, InternalOperation.U);

        await Context.SaveChangesAsync(cancellationToken);

        Context.ChangeTracker.Clear();

        return entity;
    }

    public virtual async Task<TEntity> Update(TEntity entity, string userWhoUpdated,
        CancellationToken cancellationToken)
    {
        Model.Update(entity);

        UpdateAuditOfEntity(entity, InternalOperation.U, userWhoUpdated);

        await Context.SaveChangesAsync(cancellationToken);

        Context.ChangeTracker.Clear();

        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> Update(IList<TEntity> entities, string userWhoUpdated,
        CancellationToken cancellationToken)
    {
        Context.ChangeTracker.Clear();
        Model.UpdateRange(entities);
        
        foreach (var entity in entities)
            UpdateAuditOfEntity(entity, InternalOperation.U, userWhoUpdated);

        await Context.SaveChangesAsync(cancellationToken);

        Context.ChangeTracker.Clear();

        return entities.AsEnumerable();
    }

    public virtual async Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken)
    {
        await Model.AddAsync(entity, cancellationToken);

        UpdateAuditOfEntity(entity, InternalOperation.C);

        await Context.SaveChangesAsync(cancellationToken);
        Context.ChangeTracker.Clear();
        return entity;
    }

    public virtual async Task<TEntity> Add(TEntity entity, string userWhoAdded, CancellationToken cancellationToken)
    {
        await Model.AddAsync(entity, cancellationToken);

        UpdateAuditOfEntity(entity, InternalOperation.C, userWhoAdded);

        await Context.SaveChangesAsync(cancellationToken);
        Context.ChangeTracker.Clear();
        return entity;
    }

    protected static void UpdateAuditOfEntity(TEntity entity, InternalOperation internalOperation,
        string userWhoUpdate = UserHelper.System)
    {
        if (entity is not IAudit audit) return;

        if (string.IsNullOrEmpty(userWhoUpdate))
            userWhoUpdate = UserHelper.System;

        audit.LastChangeAt = DateTime.Now;
        audit.LastChangeBy = userWhoUpdate;
        audit.InternalOperation = internalOperation;
    }

    private static void UpdateAuditOfEntity(IEnumerable<TEntity> entities, InternalOperation internalOperation,
        string? userWhoUpdate = UserHelper.System)
    {
        foreach (var entity in entities)
        {
            if (entity is not IAudit audit) continue;

            if (string.IsNullOrEmpty(userWhoUpdate))
                userWhoUpdate = UserHelper.System;

            audit.LastChangeAt = DateTime.Now;
            audit.LastChangeBy = userWhoUpdate!;
            audit.InternalOperation = internalOperation;
        }
    }
}
