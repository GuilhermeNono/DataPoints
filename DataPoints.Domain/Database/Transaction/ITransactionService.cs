using DataPoints.Domain.Enums;

namespace DataPoints.Domain.Database.Transaction;

public interface ITransactionService
{
    Task ExecuteInTransactionContextAsync(Func<Task> action,
        DbTransactionTypeEnum dbTransactionType = DbTransactionTypeEnum.ReadUncommitted,
        TransactionLogLevel logLevel = TransactionLogLevel.Explicit,
        CancellationToken cancellationToken = default);
}
