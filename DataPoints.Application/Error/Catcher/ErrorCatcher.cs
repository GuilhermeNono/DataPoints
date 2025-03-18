using System.Net;
using DataPoints.Domain.Errors;
using DataPoints.Domain.Errors.Abstractions.Interfaces;
using DataPoints.Domain.Errors.Exceptions;
using Microsoft.Extensions.Logging;
using InternalError = DataPoints.Domain.Errors.Abstractions.Error;

namespace DataPoints.Application.Error.Catcher;

public class ErrorCatcher(ILogger<ErrorCatcher> logger) : IErrorCatcher
{
    public IEnumerable<InternalError> Catch(Exception exception)
    {
        if (exception is TreatableException treatableException)
            return treatableException.ThrowHandledException();

        logger.LogError(new EventId(exception.HResult), exception.Message);
        return [new HttpError()];
    }
}
