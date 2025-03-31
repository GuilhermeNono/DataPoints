namespace DataPoints.Contract.Authentication.Me.Response;

public record MeResponse(Guid Id, string FirstName, string LastName, string Email)
{
    
}