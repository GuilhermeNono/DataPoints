using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Contract.Controller.Authentication.Refresh;
using DataPoints.Contract.Controller.Authentication.SignIn.Responses;

namespace DataPoints.Application.Members.Commands.Authentication.Refresh;

public record RefreshLoginCommand(string RefreshToken) : ICommand<SignInResponse>
{
    public static RefreshLoginCommand ToCommand(RefreshLoginRequest refreshToken)
    {
        return new RefreshLoginCommand(refreshToken.RefreshToken);
    }
}