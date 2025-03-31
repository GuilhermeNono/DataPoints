namespace DataPoints.Contract.Authentication.SignIn.Responses;

public record SignInResponse(string Token, string RefreshToken)
{
}