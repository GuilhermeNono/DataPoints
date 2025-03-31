using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Token.Jwt;

namespace DataPoints.Application.Members.Queries.Authentication.Token.Jwt;

public record TokenJwtGenerateQuery(Guid UserId) : IQuery<TokenJwtResponse>
{

}