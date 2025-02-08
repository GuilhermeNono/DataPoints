using DataPoints.Domain.Enums;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataPoints.Domain.Database.Transaction;

public interface IDatabaseTransaction
{
    IDbContextTransaction? CurrentTransaction { get; }
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransactionAsync(DbTransactionTypeEnum transactionType, CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
    Task RetryOnExceptionAsync(Func<Task> func);
}
