using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Application.Members.Queries.Authentication.Token;
using DataPoints.Application.Members.Queries.Generate.Logged;
using DataPoints.Contract.Authentication.SignIn.Responses;
using DataPoints.Crosscutting.Exceptions.Http.Unauthorized;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Authentication;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using MediatR;

namespace DataPoints.Application.Members.Commands.Authentication.Refresh;

public class RefreshLoginCommandHandler : ICommandHandler<RefreshLoginCommand, SignInResponse>
{

    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;

    private readonly ISender _sender;

    private const int MaxRevocationChainHops = 20;

    public RefreshLoginCommandHandler(IRefreshTokenRepository refreshTokenRepository, ISender sender, IUserRepository userRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _sender = sender;
        _userRepository = userRepository;
    }

    public async Task<SignInResponse> Handle(RefreshLoginCommand request, CancellationToken cancellationToken)
    {
        var refresh = await _refreshTokenRepository.FindByRefreshToken(request.RefreshToken)
                      ?? throw new RefreshNotFoundException();

        if (refresh.IsRevoked)
        {
            await RevokeDescendantChain(refresh, cancellationToken);
            throw new RefreshTokenReusedException();
        }

        if (refresh.IsExpired)
            throw new ExpiredTokenException();

        var user = await _userRepository.FindById(refresh.IdUser)
            ?? throw new UserNotFoundException(refresh.IdUser);

        var loggedPerson = await _sender.Send(new GenerateLoggedPersonByUserQuery(user.Id), cancellationToken);

        var tokenResponse = await _sender.Send(new TokenGenerateQuery(user.Id, loggedPerson), cancellationToken);

        var newRefresh = await _refreshTokenRepository.FindByRefreshToken(tokenResponse.RefreshToken);

        refresh.RevokedAt = DateTime.UtcNow;
        refresh.ReplacedByTokenId = newRefresh?.Id;
        await _refreshTokenRepository.Update(refresh, cancellationToken);

        return tokenResponse;
    }

    private async Task RevokeDescendantChain(RefreshTokenEntity refresh, CancellationToken cancellationToken)
    {
        var nextId = refresh.ReplacedByTokenId;
        var hops = 0;

        while (nextId is not null && hops++ < MaxRevocationChainHops)
        {
            var next = await _refreshTokenRepository.FindById(nextId.Value);

            if (next is null || next.IsRevoked)
                break;

            next.RevokedAt = DateTime.UtcNow;
            await _refreshTokenRepository.Update(next, cancellationToken);

            nextId = next.ReplacedByTokenId;
        }
    }
}