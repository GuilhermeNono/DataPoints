using System.Net;
using DataPoints.Domain.Errors.Exceptions;

namespace DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Abstractions;

public abstract class UnprocessableEntityException(string message) : TreatableException(message)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;
    
}
