using System.Net;
using DataPoints.Domain.Errors.Exceptions;

namespace DataPoints.Crosscutting.Exceptions.Http.NotFound.Abstractions;

public abstract class NotFoundException(string message) : TreatableException(message)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}
