using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Contract.Transaction.Insert;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Transaction.Insert;

public record TransactionInsertCommand(string ReceiverWallet, decimal Amount, LoggedPerson LoggedPerson) : ICommand<TransactionInsertResponse> 
{
    
}