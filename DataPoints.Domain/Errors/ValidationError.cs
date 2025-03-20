using System.Text.Json.Serialization;
using DataPoints.Domain.Errors.Interfaces;

namespace DataPoints.Domain.Errors;

public class ValidationError : HttpError, IValidationError
{
    public ValidationError(string? code, string? description) : base(code, description)
    {
    }

    public ValidationError()
    {
    }

    [JsonPropertyOrder(4)]
    public HttpError[]? Errors { get; set; }
}