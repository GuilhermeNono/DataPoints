using System.Globalization;
using System.Net;
using DataPoints.Domain.Errors;
using DataPoints.Domain.Errors.Abstractions.Interfaces;
using DataPoints.Domain.Errors.Exceptions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using InternalError = DataPoints.Domain.Errors.Abstractions.Error;

namespace DataPoints.Application.Error.Catcher;

public class ErrorCatcher(ILogger<ErrorCatcher> logger) : IErrorCatcher
{
    public IEnumerable<InternalError> Catch(Exception exception)
    {
        if (exception is TreatableException treatableException)
            return treatableException.ThrowHandledException();

        if (exception is ValidationException validationException)
        {
            logger.LogError(new EventId(validationException.HResult), validationException.Message);
            return
            [
                new ValidationError("validator.errorValidation", validationException.Message)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Date = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                    Errors = validationException.Errors.Select(erro => new HttpError("validator.error", erro.ErrorMessage))
                        .ToArray()
                }
            ];
        }

        logger.LogError(new EventId(exception.HResult), exception.Message);
        return [new HttpError()];
    }
}