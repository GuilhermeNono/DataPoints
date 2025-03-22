using System.Net;
using DataPoints.Domain.Errors;
using DataPoints.Domain.Errors.Abstractions;
using DataPoints.Domain.Errors.Exceptions;

namespace DataPoints.Crosscutting.Exceptions.Http.Internal.Abstractions;

public abstract class InternalException : TreatableException
{
    private readonly string _message;

    protected InternalException(string message) : base(message)
    {
        _message = message;
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

    public override IEnumerable<Error> ThrowHandledException()
    {
        Logger.Error(this, _message);
        return [new HttpError
        {
            StatusCode = (int)StatusCode,
            Code = Code,
        }];
    }
}
