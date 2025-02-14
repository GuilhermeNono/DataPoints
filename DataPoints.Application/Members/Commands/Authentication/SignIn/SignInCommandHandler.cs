using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Application.Members.Queries.Authentication.Password;
using DataPoints.Application.Members.Queries.Authentication.Token;
using DataPoints.Contract.Controller.Authentication.SignIn.Responses;
using DataPoints.Crosscutting.Exceptions.Http.Unauthorized;
using DataPoints.Domain.Repositories.Main;
using MediatR;

namespace DataPoints.Application.Members.Commands.Authentication.SignIn;

public class SignInCommandHandler : ICommandHandler<SignInCommand, SignInResponse>
{

    private readonly ISender _sender;

    private readonly IUserRepository _userRepository;

    public SignInCommandHandler(ISender sender, IUserRepository userRepository)
    {
        _sender = sender;
        _userRepository = userRepository;
    }

    public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByLogin(request.Email)
                   ?? throw new LoginNotFoundException();

        var passwordCheck = await _sender.Send(new PasswordAuthorizationQuery(user.PasswordHash, request.Password), cancellationToken);

        if(!passwordCheck.IsAuthorized)
            throw new LoginNotFoundException();

        return await _sender.Send(new TokenGenerateQuery(user.Id, request.LoggedPerson), cancellationToken);
    }
}