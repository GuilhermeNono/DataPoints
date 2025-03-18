using DataPoints.Crosscutting.Exceptions.Http.NotFound.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.NotFound;

public class WalletPublicKeyNotFoundException(string publicKey) : NotFoundException(ErrorMessage.Exception.WalletPublicKeyNotFound(publicKey))
{
    
}