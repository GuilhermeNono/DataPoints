namespace DataPoints.Contract.Password.Authorization;

public record PasswordAuthorizationResponse(string PasswordHash, bool IsAuthorized)
{

}