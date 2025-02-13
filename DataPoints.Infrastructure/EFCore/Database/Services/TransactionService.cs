using DataPoints.Domain.Database.Context;
using DataPoints.Domain.Database.Transaction;
using DataPoints.Domain.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DataPoints.Infrastructure.EFCore.Database.Services;

public class TransactionService(
    IMainContext mainContext,
    IAuditContext auditContext,
    ILogger<TransactionService> logger)
    : ITransactionService
{
    private readonly Dictionary<int, IDatabaseTransaction?> _transactions = [];
    private readonly Stack<int> _actionHashLayers = [];
    private int? _hashCodeToFinishTransaction;

    public async Task ExecuteInTransactionContextAsync(Func<Task> action, DbTransactionType transactionType,
        TransactionLogLevel logLevel, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await BeginTransaction(transactionType, cancellationToken);

        SendMessageOfTransactionStatus("|> Beginning transactions\n", logLevel);

        try
        {
            int hashCode = action.GetHashCode();

            _hashCodeToFinishTransaction ??= hashCode;
            _actionHashLayers.Push(hashCode);

            await action();


            if (_actionHashLayers.Pop() == _hashCodeToFinishTransaction)
            {
                await CommitAllDbTransactions(cancellationToken);
                SendMessageOfTransactionStatus("|> Transaction Commited\n", logLevel, false);
            }
        }
        catch (Exception)
        {
            SendMessageOfTransactionStatus("|> Rollback transaction executed\n", logLevel, false);

            await RollbackAllDbTransactions(cancellationToken);
            throw;
        }
    }

    private void SendMessageOfTransactionStatus(string message,
        TransactionLogLevel logLevel = TransactionLogLevel.Explicit, bool whenTransactionsNotExist = true)
    {
        if (logLevel is TransactionLogLevel.Implicit)
            return;

        if (whenTransactionsNotExist ? _transactions.IsNullOrEmpty() : !_transactions.IsNullOrEmpty())
            logger.LogInformation("{Message}", message);
    }

    private async Task BeginTransaction(DbTransactionType transactionType, CancellationToken cancellationToken)
    {
        await ComputeDbTransaction(mainContext, transactionType, cancellationToken);
        await ComputeDbTransaction(auditContext, transactionType, cancellationToken);
    }

    private async Task ComputeDbTransaction(IDatabaseContext context, DbTransactionType transactionType, CancellationToken cancellationToken)
    {
        if (!HashKeyExistInTransactionList(context.GetHashCode()))
        {
            await mainContext.BeginTransactionAsync(transactionType, cancellationToken);
            _transactions.Add(context.GetHashCode(), context);
        }
    }

    private async Task CommitAllDbTransactions(CancellationToken cancellationToken)
    {
        await Task.WhenAll(_transactions.Select(x => x.Value!.CommitTransactionAsync(cancellationToken)).ToArray());
    }

    private async Task RollbackAllDbTransactions(CancellationToken cancellationToken)
    {
        foreach (var (provider, transaction) in _transactions)
        {
            if (transaction is null) continue;

            await transaction.RollbackTransactionAsync(cancellationToken);
            _transactions.Remove(provider);
        }
    }

    private bool HashKeyExistInTransactionList(int hashKey) => _transactions.ContainsKey(hashKey);
}
