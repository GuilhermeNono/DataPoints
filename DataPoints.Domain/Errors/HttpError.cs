using DataPoints.Domain.Errors.Abstractions;

namespace DataPoints.Domain.Errors;

public class HttpError : Error
{
    public HttpError(string? code, string? description) : base(code, description)
    {
    }

    public HttpError()
    {
    }
}