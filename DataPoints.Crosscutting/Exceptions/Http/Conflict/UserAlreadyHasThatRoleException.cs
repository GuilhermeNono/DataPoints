using DataPoints.Crosscutting.Exceptions.Http.Conflict.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.Conflict;

public class UserAlreadyHasThatRoleException(string role) : ConflictException(ErrorMessage.Exception.UserAlreadyHasThatRole(role))
{
    
}
