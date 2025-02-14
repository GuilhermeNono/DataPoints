using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Token.Refresh;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Queries.Authentication.Token.Refresh;

public record TokenRefreshGenerateQuery(Guid IdUser, LoggedPerson LoggedPerson) : IQuery<TokenRefreshResponse>
{

}