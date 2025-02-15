using System.Net;
using DataPoints.Domain.Errors;
using DataPoints.Domain.Errors.Abstractions.Interfaces;
using DataPoints.Domain.Errors.Exceptions;
using Microsoft.Extensions.Logging;
using InternalError = DataPoints.Domain.Errors.Abstractions.Error;

namespace DataPoints.Application.Error.Catcher;

public class ErrorCatcher : IErrorCatcher
{
    
    private readonly ILogger<ErrorCatcher> _logger;

    public ErrorCatcher(ILogger<ErrorCatcher> logger)
    {
        _logger = logger;
    }

    public IEnumerable<InternalError> Catch(Exception exception)
    {
        if (exception is TreatableException treatableException)
            return treatableException.ThrowHandledException();

        _logger.LogError(new EventId(exception.HResult), exception.Message);
        return new List<InternalError>
        {
            new HttpError
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Code = nameof(HttpStatusCode.InternalServerError),
                Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Description = "Houve um problema interno, por favor tente mais tarde."
            }
        };
    }
}
