using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Contract.Transaction.Insert;
using DataPoints.Contract.Transaction.Insert.Response;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Transaction.Insert;

public record TransactionInsertCommand(string ReceiverWallet, string Signature, decimal Amount, LoggedPerson LoggedPerson) : ICommand<TransactionInsertResponse> 
{
    
}