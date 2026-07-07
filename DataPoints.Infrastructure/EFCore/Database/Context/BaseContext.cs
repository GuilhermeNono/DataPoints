using System.Data;
using DataPoints.Domain.Database.Context;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace DataPoints.Infrastructure.EFCore.Database.Context;

public abstract class BaseContext<TContext>(DbContextOptions<TContext> options, ILogger<BaseContext<TContext>> logger)
    : DbContext(options), IDatabaseContext where TContext : DbContext
{

    public ILogger<BaseContext<TContext>> Logger { get; set; } = logger;
    public IDbContextTransaction? CurrentTransaction { get; private set; }

    private bool _ownsTransaction;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!EnvironmentHelper.IsDevelopmentEnvironment)
            optionsBuilder.EnableDetailedErrors(false);
        else
            optionsBuilder.EnableSensitiveDataLogging();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        if (CurrentTransaction is not null) return CurrentTransaction;

        CurrentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        _ownsTransaction = true;
        return CurrentTransaction;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(DbTransactionType transactionType, CancellationToken cancellationToken)
    {
        if (CurrentTransaction is not null) return CurrentTransaction;

        CurrentTransaction = await Database.BeginTransactionAsync(SwitchTransactionType(transactionType), cancellationToken);
        _ownsTransaction = true;
        return CurrentTransaction;
    }

    public async Task<IDbContextTransaction> EnlistTransactionAsync(IDbContextTransaction ownerTransaction, CancellationToken cancellationToken)
    {
        if (CurrentTransaction is not null) return CurrentTransaction;

        CurrentTransaction = await Database.UseTransactionAsync(ownerTransaction.GetDbTransaction(), cancellationToken: cancellationToken);
        _ownsTransaction = false;
        return CurrentTransaction;
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            if (_ownsTransaction && CurrentTransaction is not null)
                await CurrentTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await DisposeTransaction();
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (_ownsTransaction && CurrentTransaction != null)
                await CurrentTransaction.RollbackAsync(cancellationToken)!;
        }
        finally
        {
            await DisposeTransaction();
        }
    }

    private async Task DisposeTransaction()
    {
        if (CurrentTransaction != null)
        {
            await CurrentTransaction.DisposeAsync();
            CurrentTransaction = null;
        }

        _ownsTransaction = false;
    }

    protected static void LowercaseIdentifiers(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName is not null)
                entityType.SetTableName(tableName.ToLowerInvariant());

            var viewName = entityType.GetViewName();
            if (viewName is not null)
                entityType.SetViewName(viewName.ToLowerInvariant());

            foreach (var property in entityType.GetProperties())
            {
                var columnName = property.GetColumnName();
                if (columnName is not null)
                    property.SetColumnName(columnName.ToLowerInvariant());
            }
        }
    }

    private static IsolationLevel SwitchTransactionType(DbTransactionType transactionType)
    {
        var result = IsolationLevel.ReadUncommitted;

        switch (transactionType)
        {
            case DbTransactionType.ReadCommit:
            {
                result = IsolationLevel.ReadCommitted;
                break;
            }
            case DbTransactionType.ReadUncommitted:
            {
                result = IsolationLevel.ReadUncommitted;
                break;
            }
            case DbTransactionType.RepeatableRead:
            {
                result = IsolationLevel.RepeatableRead;
                break;
            }
            case DbTransactionType.Serializable:
            {
                result = IsolationLevel.Serializable;
                break;
            }
            case DbTransactionType.NoTransaction:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null);
        }

        return result;
    }
}
