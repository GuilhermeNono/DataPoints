using System.Security.Cryptography;
using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Token.Refresh;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Queries.Authentication.Token.Refresh;

public class TokenRefreshGenerateQueryHandler : IQueryHandler<TokenRefreshGenerateQuery, TokenRefreshResponse>
{

    private readonly IUserRepository _userRepository;

    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public TokenRefreshGenerateQueryHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<TokenRefreshResponse> Handle(TokenRefreshGenerateQuery request, CancellationToken cancellationToken)
    {
        if(!await _userRepository.Exists(request.IdUser))
            throw new UserNotFoundException(request.IdUser);

        var token = GenerateToken();

        var entity = new RefreshTokenEntity()
        {
            Token = token,
            IdUser = request.IdUser
        };

        await _refreshTokenRepository.Add(entity, request.LoggedPerson.Name, cancellationToken);

        return new TokenRefreshResponse(token);
    }

    private string GenerateToken()
    {
        var randomNumber = new byte[32];
        using var numberGenerator = RandomNumberGenerator.Create();
        numberGenerator.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
}