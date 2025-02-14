using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Password.Authorization;
using static BCrypt.Net.BCrypt;

namespace DataPoints.Application.Members.Queries.Authentication.Password;

public class PasswordAuthorizationQueryHandler : IQueryHandler<PasswordAuthorizationQuery, PasswordAuthorizationResponse>
{
    public Task<PasswordAuthorizationResponse> Handle(PasswordAuthorizationQuery request, CancellationToken cancellationToken)
    {
        var passwordChecked = new PasswordAuthorizationResponse(request.RealPassword,
            Verify(request.SendedPassword, request.RealPassword));

        return Task.FromResult(passwordChecked);
    }
}