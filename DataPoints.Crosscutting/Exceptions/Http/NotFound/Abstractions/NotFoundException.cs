using System.Net;
using DataPoints.Domain.Errors.Exceptions;

namespace DataPoints.Crosscutting.Exceptions.Http.NotFound.Abstractions;

public abstract class NotFoundException : TreatableException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    protected NotFoundException()
    {
    }

    protected NotFoundException(string? message) : base(message)
    {
    }
}
