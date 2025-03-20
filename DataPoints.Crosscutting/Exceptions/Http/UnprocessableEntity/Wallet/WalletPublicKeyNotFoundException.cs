using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Wallet;

public class WalletPublicKeyNotFoundException(string publicKey) : UnprocessableEntityException(ErrorMessage.Exception.WalletPublicKeyNotFound(publicKey))
{
    
}