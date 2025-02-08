using System.Net;

namespace DataPoints.Crosscutting.Exceptions.Http.Abstractions;

public interface IHttpResponseException
{
    public HttpStatusCode StatusCode { get; }
    public string? Code { get; }
}
