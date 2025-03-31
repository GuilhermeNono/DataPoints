

using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Transaction.Invalidate;

public record TransactionInvalidateCommand(Guid BlockId, LoggedPerson LoggedPerson) : ICommand
{
}