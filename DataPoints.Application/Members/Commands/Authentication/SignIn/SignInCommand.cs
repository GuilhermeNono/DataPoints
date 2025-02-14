using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Contract.Controller.Authentication.SignIn.Responses;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Authentication.SignIn;

public record SignInCommand(string Email, string Password, LoggedPerson LoggedPerson) : ICommand<SignInResponse>
{
}