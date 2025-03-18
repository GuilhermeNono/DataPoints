using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Transaction.Insert;

public record TransactionInsertCommand(string ReceiverPublicKey, decimal Amount, LoggedPerson LoggedPerson) : ICommand<Guid> 
{
    
}