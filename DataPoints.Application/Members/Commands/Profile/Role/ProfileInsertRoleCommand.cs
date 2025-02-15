using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Profile.Role;

public record ProfileInsertRoleCommand(Guid IdUser, string Role, LoggedPerson LoggedPerson) : ICommand
{
    
}
