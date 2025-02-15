using DataPoints.Domain.Errors.Exceptions;

namespace DataPoints.Crosscutting.Exceptions.Http.BadRequest.Abstractions;

public abstract class BadRequestException(string message) : TreatableException(message);
