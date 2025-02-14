using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Controller.Authentication.SignIn.Responses;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Queries.Authentication.Token;

public record TokenGenerateQuery(Guid UserId, LoggedPerson LoggedPerson) : IQuery<SignInResponse>
{

}