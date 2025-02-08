using System.Net;
using DataPoints.Crosscutting.Exceptions.Http.Abstractions;

namespace DataPoints.Crosscutting.Exceptions.Http.Internal.Abstractions;

public abstract class InternalException : Exception, IHttpResponseException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string? Code => nameof(StatusCode);

    protected InternalException()
    {
    }

    protected InternalException(string? message) : base(message)
    {
    }
}
