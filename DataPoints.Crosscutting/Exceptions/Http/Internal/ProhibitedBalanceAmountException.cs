using DataPoints.Crosscutting.Exceptions.Http.Internal.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.Internal;

public class ProhibitedBalanceAmountException(decimal points) : InternalException(ErrorMessage.Exception.ProhibitedBalanceAmount(points))
{
    
}