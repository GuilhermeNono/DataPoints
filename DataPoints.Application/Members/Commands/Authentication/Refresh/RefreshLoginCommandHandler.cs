using System.Security;
using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Application.Members.Queries.Authentication.Token;
using DataPoints.Application.Members.Queries.Generate.Logged;
using DataPoints.Contract.Controller.Authentication.SignIn.Responses;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Authentication;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;
using DataPoints.Domain.Repositories.Main;
using MediatR;

namespace DataPoints.Application.Members.Commands.Authentication.Refresh;

public class RefreshLoginCommandHandler : ICommandHandler<RefreshLoginCommand, SignInResponse>
{
    
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;

    private readonly ISender _sender;

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
        
        if(refresh.IsExpired)
            throw new SecurityException("Refresh token expirado");
        
        var user = await _userRepository.FindById(refresh.IdUser)
            ?? throw new UserNotFoundException(refresh.IdUser);

        var loggedPerson = await _sender.Send(new GenerateLoggedPersonByUserQuery(user.Id), cancellationToken);

        return await _sender.Send(new TokenGenerateQuery(user.Id, loggedPerson), cancellationToken);
    }
}