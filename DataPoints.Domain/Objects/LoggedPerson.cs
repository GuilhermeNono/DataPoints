using System.Security.Claims;
using DataPoints.Domain.Helpers;

namespace DataPoints.Domain.Objects;

public class LoggedPerson
{
    public Guid? Id { get; init; }
    public string Name { get; init; } = UserHelper.Anonymous;
    public IEnumerable<string> Roles { get; init; } = [];
    public bool IsAuthenticated => Id is not null; 
    public bool IsAdministrator => Roles.Contains(RoleHelper.Administrator);
    public static LoggedPerson Anonymous() => new ();
    public static LoggedPerson System() => new (){Name = UserHelper.System};
}
