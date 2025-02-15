using System.Net;
using DataPoints.Domain.Errors.Exceptions;

namespace DataPoints.Crosscutting.Exceptions.Http.Unauthorized.Abstractions;

public abstract class UnauthorizedException(string message) : TreatableException(message)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
}
