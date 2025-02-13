using DataPoints.Domain.Errors.Abstractions;

namespace DataPoints.Domain.Errors.Interfaces;

public interface IValidationError : IHttpError
{
    HttpError[]? Errors { get; set; }
}