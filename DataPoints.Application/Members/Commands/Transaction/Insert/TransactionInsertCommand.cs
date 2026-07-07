using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Contract.Transaction.Insert;
using DataPoints.Contract.Transaction.Insert.Response;
using DataPoints.Domain.Annotations;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Transaction.Insert;

[TransactionType(DbTransactionType.Serializable)]
public record TransactionInsertCommand(
    string ReceiverWallet,
    string Signature,
    decimal Amount,
    Guid IdempotencyKey,
    DateTime IssuedAtUtc,
    LoggedPerson LoggedPerson) : ICommand<TransactionInsertResponse>, IIdempotentCommand
{
}