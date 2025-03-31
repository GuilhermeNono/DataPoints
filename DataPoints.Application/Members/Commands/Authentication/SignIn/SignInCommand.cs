using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Contract.Authentication.SignIn.Requests;
using DataPoints.Contract.Authentication.SignIn.Responses;
using DataPoints.Domain.Objects;
using MediatR;

namespace DataPoints.Application.Members.Commands.Authentication.SignIn;

public record SignInCommand(string Email, string Password, LoggedPerson LoggedPerson) : ICommand<SignInResponse>
{
    public static SignInCommand ToCommand(SignInRequest user, LoggedPerson loggedPerson)
    {
        return new SignInCommand(user.Email, user.Password, loggedPerson);
    }
}
