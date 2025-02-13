using DataPoints.Domain.Errors.Abstractions;
using DataPoints.Domain.Errors.Exceptions.Http;

namespace DataPoints.Domain.Errors.Exceptions;

public interface ITreatableException : IHttpException
{
    IEnumerable<Error> ThrowHandledException();
}
