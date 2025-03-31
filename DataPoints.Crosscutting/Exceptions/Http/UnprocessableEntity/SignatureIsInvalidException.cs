using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity;

public class SignatureIsInvalidException() : UnprocessableEntityException(ErrorMessage.Exception.SignatureIsInvalid())
{
    
}