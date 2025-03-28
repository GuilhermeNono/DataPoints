using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.Internal;

public class WalletHashNotFoundException(string hash) : UnprocessableEntityException(ErrorMessage.Exception.WalletHashNotFound(hash))
{
    
}