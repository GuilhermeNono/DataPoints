using DataPoints.Crosscutting.Exceptions.Http.NotFound.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.NotFound;

public class WalletUserNotFoundException(Guid userId) : NotFoundException(ErrorMessage.Exception.WalletUserNotFound(userId))
{
    
}