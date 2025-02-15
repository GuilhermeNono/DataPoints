using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;

public class UserEmailNotFoundException() : UnprocessableEntityException(ErrorMessage.Exception.UserEmailNotFound())
{
    
}
