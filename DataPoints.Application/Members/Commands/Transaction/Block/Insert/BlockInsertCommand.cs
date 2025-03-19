using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Transaction.Block.Insert;

public record BlockInsertCommand(IEnumerable<Guid> Transactions, LoggedPerson LoggedPerson) : ICommand<string>
{
    
}