using System.Net;
using DataPoints.Crosscutting.Exceptions.Http.Abstractions;

namespace DataPoints.Crosscutting.Exceptions.Http.NotFound.Abstractions;

public abstract class NotFoundException : Exception, IHttpResponseException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string? Code => nameof(StatusCode);

    protected NotFoundException()
    {
    }

    protected NotFoundException(string? message) : base(message)
    {
    }
}
