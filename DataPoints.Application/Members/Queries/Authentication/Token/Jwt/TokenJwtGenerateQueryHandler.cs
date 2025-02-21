using System.Security.Claims;
using System.Text;
using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Token.Jwt.Responses;
using DataPoints.Crosscutting.Configurations;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;
using DataPoints.Domain.Repositories.Main;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace DataPoints.Application.Members.Queries.Authentication.Token.Jwt;

public class TokenJwtGenerateQueryHandler : IQueryHandler<TokenJwtGenerateQuery, TokenJwtResponse>
{

    private readonly IUserRepository _userRepository;

    private readonly IJwtConfiguration _jwtConfiguration;

    public TokenJwtGenerateQueryHandler(IUserRepository userRepository, IJwtConfiguration jwtConfiguration)
    {
        _userRepository = userRepository;
        _jwtConfiguration = jwtConfiguration;
    }

    public async Task<TokenJwtResponse> Handle(TokenJwtGenerateQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindById(request.UserId)
                   ?? throw new UserNotFoundException(request.UserId);

        var claims = new Dictionary<string, object>()
        {
            { JwtRegisteredClaimNames.Sub, user.Id.ToString() },
            { "Email", user.Email},
            { JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() }
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = claims,
            Audience = _jwtConfiguration.Audience,
            Issuer = _jwtConfiguration.Issuer,
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpirationInMinutes),
            SigningCredentials = signingCredentials
        };

        var token = new JsonWebTokenHandler()
            .CreateToken(tokenDescriptor);

        return new TokenJwtResponse(token);
    }
}