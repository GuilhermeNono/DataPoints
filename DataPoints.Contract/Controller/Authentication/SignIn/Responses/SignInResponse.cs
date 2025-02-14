namespace DataPoints.Contract.Controller.Authentication.SignIn.Responses;

public record SignInResponse(string Token, string RefreshToken)
{
}