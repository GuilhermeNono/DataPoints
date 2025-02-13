using System.Data;
using DataPoints.Domain.Database.Context;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace DataPoints.Infrastructure.EFCore.Database.Context;

public abstract class BaseContext<TContext>(DbContextOptions<TContext> options, ILogger<BaseContext<TContext>> logger)
    : DbContext(options), IDatabaseContext where TContext : DbContext
{

    public ILogger<BaseContext<TContext>> Logger { get; set; } = logger;
    public IDbContextTransaction? CurrentTransaction { get; private set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!EnvironmentHelper.IsDevelopmentEnvironment)
            optionsBuilder.EnableDetailedErrors(false);
        else
            optionsBuilder.EnableSensitiveDataLogging();

        optionsBuilder.ConfigureWarnings(w => w.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return CurrentTransaction ??= await Database.BeginTransactionAsync(IsolationLevel.ReadUncommitted, cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(DbTransactionType transactionType, CancellationToken cancellationToken)
    {
        return CurrentTransaction ??= await Database.BeginTransactionAsync(SwitchTransactionType(transactionType), cancellationToken);
    }



    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            CurrentTransaction?.CommitAsync(cancellationToken);
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
            if(CurrentTransaction != null)
                await CurrentTransaction.RollbackAsync(cancellationToken)!;
        }
        finally
        {
            await DisposeTransaction();
        }
    }
    
    public async Task RetryOnExceptionAsync(Func<Task> func)
    {
        await Database.CreateExecutionStrategy().ExecuteAsync(func);
    }

    private async Task DisposeTransaction()
    {
        if (CurrentTransaction != null)
        {
            await CurrentTransaction.DisposeAsync();
            CurrentTransaction = null;
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
            case DbTransactionType.NoTransaction:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null);
        }

        return result;
    }
}
