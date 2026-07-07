using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Application.Members.Queries.Authentication.Password;
using DataPoints.Application.Members.Queries.Authentication.Token;
using DataPoints.Contract.Authentication.SignIn.Responses;
using DataPoints.Crosscutting.Exceptions.Http.Unauthorized;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DataPoints.Application.Members.Commands.Authentication.SignIn;

public class SignInCommandHandler : ICommandHandler<SignInCommand, SignInResponse>
{

    private readonly ISender _sender;

    private readonly IUserRepository _userRepository;
    private readonly UserManager<UserEntity> _userManager;

    public SignInCommandHandler(ISender sender, IUserRepository userRepository, UserManager<UserEntity> userManager)
    {
        _sender = sender;
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByNormalizedEmail(request.Email)
                   ?? throw new LoginNotFoundException();

        if (await _userManager.IsLockedOutAsync(user))
            throw new LoginNotFoundException();

        var passwordCheck = await _sender.Send(new PasswordAuthorizationQuery(user.PasswordHash, request.Password), cancellationToken);

        if (!passwordCheck.IsAuthorized)
        {
            await _userManager.AccessFailedAsync(user);
            throw new LoginNotFoundException();
        }

        await _userManager.ResetAccessFailedCountAsync(user);

        return await _sender.Send(new TokenGenerateQuery(user.Id, request.LoggedPerson), cancellationToken);
    }
}