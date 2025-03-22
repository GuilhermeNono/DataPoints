using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Wallets.Balance.Update;

public record WalletBalanceUpdateCommand(Guid WalletId, LoggedPerson LoggedPerson) : ICommand<decimal>
{
    
}