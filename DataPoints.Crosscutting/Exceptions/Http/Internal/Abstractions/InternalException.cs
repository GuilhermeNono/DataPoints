using System.Net;
using DataPoints.Domain.Errors.Exceptions;

namespace DataPoints.Crosscutting.Exceptions.Http.Internal.Abstractions;

public abstract class InternalException : TreatableException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

}
