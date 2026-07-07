using System.Net;
using DataPoints.Domain.Errors.Exceptions;

namespace DataPoints.Crosscutting.Exceptions.Http.Forbidden.Abstractions;

public abstract class ForbiddenException(string message) : TreatableException(message)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
}
