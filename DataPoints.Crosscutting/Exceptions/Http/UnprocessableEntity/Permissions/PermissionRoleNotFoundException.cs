using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Permissions;

public class PermissionRoleNotFoundException(string role) : UnprocessableEntityException(ErrorMessage.Exception.PermissionRoleNotFound(role))
{
    
}
