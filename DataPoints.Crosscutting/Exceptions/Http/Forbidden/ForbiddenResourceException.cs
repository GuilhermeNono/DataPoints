using DataPoints.Crosscutting.Exceptions.Http.Forbidden.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.Forbidden;

public class ForbiddenResourceException() : ForbiddenException(ErrorMessage.Exception.ForbiddenResource())
{
}
