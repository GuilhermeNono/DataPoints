using DataPoints.Crosscutting.Exceptions.Http.NotFound.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Wallet;

public class WalletNotFoundException : NotFoundException
{
    public WalletNotFoundException(Guid walletId) : base(ErrorMessage.Exception.WalletNotFound(walletId))
    {
    }

    public WalletNotFoundException() : base(ErrorMessage.Exception.WalletNotFound())
    {
    }
}