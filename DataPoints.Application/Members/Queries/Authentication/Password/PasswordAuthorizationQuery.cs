using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Password.Authorization;

namespace DataPoints.Application.Members.Queries.Authentication.Password;

public record PasswordAuthorizationQuery(string RealPassword, string SendedPassword) : IQuery<PasswordAuthorizationResponse>
{

}