using System.Net;
using DataPoints.Domain.Errors.Exceptions;

namespace DataPoints.Crosscutting.Exceptions.Http.NotFound.Abstractions;

public abstract class NotFoundException : TreatableException
{
    protected NotFoundException(string message) : base(message)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}
