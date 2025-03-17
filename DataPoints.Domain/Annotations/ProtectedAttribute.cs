using DataPoints.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace DataPoints.Domain.Annotations;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class ProtectedAttribute : AuthorizeAttribute
{
    public ProtectedAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
    
    public ProtectedAttribute(params RoleProfile[] roles) : this(roles.Select(x => x.ToString().ToUpper()).ToArray())
    {
    }
}