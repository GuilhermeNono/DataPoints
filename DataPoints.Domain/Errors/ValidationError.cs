using System.Text.Json.Serialization;
using DataPoints.Domain.Errors.Interfaces;

namespace DataPoints.Domain.Errors;

public class ValidationError : HttpError, IValidationError
{
    [JsonPropertyOrder(4)]
    public HttpError[]? Errors { get; set; }
}