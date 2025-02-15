using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Contract.Controller.Authentication.SignUp.Requests;
using DataPoints.Contract.Controller.Authentication.SignUp.Responses;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Commands.Authentication.SignUp;

public record SignUpCommand(
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string DocumentNumber,
    string Email,
    string Password,
    LoggedPerson LoggedPerson) : ICommand<SignUpResponse>
{
    public static SignUpCommand ToCommand(SignUpRequest user, LoggedPerson loggedPerson)
    {
        return new SignUpCommand(user.FirstName, user.LastName, user.BirthDate, user.DocumentNumber, user.Email, user.Password, loggedPerson);
    }
}
