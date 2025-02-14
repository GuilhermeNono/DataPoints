using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Application.Members.Queries.Authentication.Token.Jwt;
using DataPoints.Application.Members.Queries.Authentication.Token.Refresh;
using DataPoints.Contract.Controller.Authentication.SignIn.Responses;
using MediatR;

namespace DataPoints.Application.Members.Queries.Authentication.Token;

public class TokenGenerateQueryHandler : IQueryHandler<TokenGenerateQuery, SignInResponse>
{

    private readonly ISender _sender;

    public TokenGenerateQueryHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task<SignInResponse> Handle(TokenGenerateQuery request, CancellationToken cancellationToken)
    {
        var token = await _sender.Send(new TokenJwtGenerateQuery(request.UserId), cancellationToken);

        var refresh = await _sender.Send(new TokenRefreshGenerateQuery(request.UserId, request.LoggedPerson), cancellationToken);

        return new SignInResponse(token.JsonWebToken, refresh.RefreshToken);
    }
}