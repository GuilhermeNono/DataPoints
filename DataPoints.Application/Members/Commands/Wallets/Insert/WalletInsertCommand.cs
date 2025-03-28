using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Contract.Wallet.Private;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Wallets.Insert;

public record WalletInsertCommand(Guid IdUser, LoggedPerson LoggedPerson): ICommand<WalletPrivateKeyResponse>
{
    
}
