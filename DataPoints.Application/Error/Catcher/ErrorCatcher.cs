using System.Net;
using DataPoints.Domain.Errors;
using DataPoints.Domain.Errors.Abstractions.Interfaces;
using DataPoints.Domain.Errors.Exceptions;
using InternalError = DataPoints.Domain.Errors.Abstractions.Error;

namespace DataPoints.Application.Error.Catcher;

public class ErrorCatcher : IErrorCatcher
{
    public IEnumerable<InternalError> Catch(Exception exception)
    {
        if (exception is TreatableException treatableException)
            return treatableException.ThrowHandledException();

        return new List<InternalError>
        {
            new HttpError
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Code = nameof(HttpStatusCode.InternalServerError),
                Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Description = $"Houve um problema interno, por favor tente mais tarde."
            }
        };
    }
}