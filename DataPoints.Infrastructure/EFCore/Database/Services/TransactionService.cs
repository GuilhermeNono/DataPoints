using DataPoints.Domain.Database.Context;
using DataPoints.Domain.Database.Transaction;
using DataPoints.Domain.Enums;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DataPoints.Infrastructure.EFCore.Database.Services;

public class TransactionService : ITransactionService
{
    private const int MaxAttempts = 3;

    private readonly IReadOnlyList<IDatabaseContext> _contexts;
    private readonly ILogger<TransactionService> _logger;

    private int _depth;

    public TransactionService(IMainContext mainContext, IAuditContext auditContext, ILogger<TransactionService> logger)
    {
        _contexts = [mainContext, auditContext];
        _logger = logger;
    }

    public async Task ExecuteInTransactionContextAsync(Func<Task> action, DbTransactionType transactionType,
        TransactionLogLevel logLevel, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (_depth > 0)
        {
            _depth++;
            try
            {
                await action();
            }
            finally
            {
                _depth--;
            }

            return;
        }

        for (var attempt = 1; ; attempt++)
        {
            _depth++;
            try
            {
                await BeginAllDbTransactionsAsync(transactionType, cancellationToken);
                SendMessageOfTransactionStatus("|> Beginning transactions\n", logLevel);

                await action();

                await CommitAllDbTransactionsAsync(cancellationToken);
                SendMessageOfTransactionStatus("|> Transaction Commited\n", logLevel);
                return;
            }
            catch (Exception e) when (attempt < MaxAttempts && IsTransientSerializationFailure(e))
            {
                await RollbackAllDbTransactionsAsync(cancellationToken);
                _logger.LogWarning(
                    "|> Falha de serialização/deadlock detectada, retentando (tentativa {Attempt}/{MaxAttempts})",
                    attempt, MaxAttempts);
                await Task.Delay(TimeSpan.FromMilliseconds(50 * attempt), cancellationToken);
            }
            catch (Exception)
            {
                SendMessageOfTransactionStatus("|> Rollback transaction executed\n", logLevel);
                await RollbackAllDbTransactionsAsync(cancellationToken);
                throw;
            }
            finally
            {
                _depth--;
            }
        }
    }

    private async Task BeginAllDbTransactionsAsync(DbTransactionType transactionType, CancellationToken cancellationToken)
    {
        var owner = _contexts[0];
        var ownerTransaction = await owner.BeginTransactionAsync(transactionType, cancellationToken);

        foreach (var context in _contexts.Skip(1))
            await context.EnlistTransactionAsync(ownerTransaction, cancellationToken);
    }

    private void SendMessageOfTransactionStatus(string message, TransactionLogLevel logLevel)
    {
        if (logLevel is TransactionLogLevel.Implicit)
            return;

        _logger.LogInformation("{Message}", message);
    }

    private async Task CommitAllDbTransactionsAsync(CancellationToken cancellationToken)
    {
        foreach (var context in _contexts)
            await context.SaveChangesAsync(cancellationToken);

        foreach (var context in _contexts)
            await context.CommitTransactionAsync(cancellationToken);
    }

    private async Task RollbackAllDbTransactionsAsync(CancellationToken cancellationToken)
    {
        foreach (var context in _contexts)
            await context.RollbackTransactionAsync(cancellationToken);
    }

    private static bool IsTransientSerializationFailure(Exception exception)
    {
        var current = exception;
        while (current is not null)
        {
            if (current is PostgresException { SqlState: PostgresErrorCodes.SerializationFailure or PostgresErrorCodes.DeadlockDetected })
                return true;

            current = current.InnerException;
        }

        return false;
    }
}
