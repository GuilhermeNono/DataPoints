using System.Security.Claims;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Objects;
using Microsoft.IdentityModel.JsonWebTokens;

namespace DataPoints.Domain.Helpers;

public static class JwtHelper
{
    private static string GetFirstNameFromClaims(IEnumerable<Claim> claims) => claims
        .Where(claim => claim.Type == JwtRegisteredClaimNames.Name)
        .Select(claim => claim.Value.Trim()).First();

    private static IEnumerable<string> GetFirstRolesFromClaims(IEnumerable<Claim> claims) => claims
        .Where(claim => claim.Type == ClaimTypes.Role)
        .Select(claim => claim.Value);
    
    private static Guid GetLoggedPersonId(IEnumerable<Claim> claims)
    {
        return claims.Where(claim => claim.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.CurrentCultureIgnoreCase))
            .Select(claim => new Guid(claim.Value)).FirstOrDefault();
    }

    private static string GetLoggedPersonName(IEnumerable<Claim> claims)
    {
        var claimsOrdered = claims.ToList();
        return claimsOrdered.Count != 0
            ? GetFirstNameFromClaims(claimsOrdered)
            : string.Empty;
    }
    
    private static IEnumerable<string> GetLoggedPersonRoles(IEnumerable<Claim> claims)
    {
        var claimsOrdered = claims.ToList();
        return claimsOrdered.Count != 0
            ? GetFirstRolesFromClaims(claimsOrdered)
            : [];
    }
    
    public static LoggedPerson CreateAuthenticatedPerson(ClaimsPrincipal user)
    {
        if (user?.Identity?.IsAuthenticated is false)
            return LoggedPerson.Anonymous();
        
        return new LoggedPerson
        {
            Id = GetLoggedPersonId(user.Claims),
            Name = GetLoggedPersonName(user.Claims),
            Roles = GetLoggedPersonRoles(user.Claims),
        };
    }
}
