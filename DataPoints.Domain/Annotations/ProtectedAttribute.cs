using Microsoft.AspNetCore.Authorization;

namespace DataPoints.Domain.Annotations;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ProtectedAttribute : AuthorizeAttribute
{
    public ProtectedAttribute(string? roles = null)
    {
        AuthenticationSchemes = "Bearer";
        Roles = roles;
    }
}