using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Authentication;

public class RefreshNotFoundException() : UnprocessableEntityException(ErrorMessage.Exception.RefreshNotFound())
{
    
}