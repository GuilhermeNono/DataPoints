using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Domain.Objects;
using DataPoints.Domain.Objects.Security;

namespace DataPoints.Application.Members.Commands.Block.Insert;

public record BlockInsertCommand(IEnumerable<Guid> Transactions, SecurityGroupKey SecurityKeys, LoggedPerson LoggedPerson) : ICommand<Guid>
{
    
}