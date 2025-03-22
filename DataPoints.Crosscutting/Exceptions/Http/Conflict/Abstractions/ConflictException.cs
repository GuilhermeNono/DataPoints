using System.Net;
using DataPoints.Domain.Errors.Exceptions;

namespace DataPoints.Crosscutting.Exceptions.Http.Conflict.Abstractions;

public abstract class ConflictException(string message) : TreatableException(message)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}
