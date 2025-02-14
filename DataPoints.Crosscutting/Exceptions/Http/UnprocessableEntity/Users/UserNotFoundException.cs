using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;

public class UserNotFoundException : UnprocessableEntityException
{
    public UserNotFoundException(Guid id) : base(ErrorMessage.Exception.UserNotFound(id))
    {
    }
}