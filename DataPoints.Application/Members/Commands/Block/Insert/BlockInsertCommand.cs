using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Block.Insert;

public record BlockInsertCommand(IEnumerable<Guid> Transactions, LoggedPerson LoggedPerson) : ICommand<Guid>
{
    
}