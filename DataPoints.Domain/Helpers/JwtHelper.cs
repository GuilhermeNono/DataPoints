using System.Security.Claims;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Objects;

namespace DataPoints.Domain.Helpers;

public static class JwtHelper
{
    private static string GetFirstNameFromClaims(IEnumerable<Claim> claims) => claims
        .Where(claim => claim.Type == ClaimTypes.Name)
        .Select(claim => claim.Value).First();

    private static string GetLastNameFromClaims(IEnumerable<Claim> claims) => claims
        .Where(claim => claim.Type == nameof(ClaimType.LastName))
        .Select(claim => claim.Value).FirstOrDefault(string.Empty);
    
    private static IEnumerable<string> GetFirstRolesFromClaims(IEnumerable<Claim> claims) => claims
        .Where(claim => claim.Type == ClaimTypes.Role)
        .Select(claim => claim.Value);
    
    private static long GetLoggedPersonId(IEnumerable<Claim> claims)
    {
        return claims.Where(claim => claim.Type == ClaimType.Id.ToString())
            .Select(claim => Convert.ToInt64(claim.Value)).FirstOrDefault();
    }

    private static string GetLoggedPersonName(IEnumerable<Claim> claims)
    {
        var claimsOrdered = claims.ToList();
        return claimsOrdered.Count != 0
            ? string.Concat(GetFirstNameFromClaims(claimsOrdered), GetLastNameFromClaims(claimsOrdered))
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
