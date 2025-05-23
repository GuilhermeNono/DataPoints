using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Users.Responses;
using DataPoints.Crosscutting.Exceptions.Http.NotFound;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Queries.User.GetById;

public class UserGetByIdQueryHandler : IQueryHandler<UserGetByIdQuery, UserGetResponse>
{
    private readonly IUserRepository _userRepository;

    public UserGetByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserGetResponse> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.FindById(request.Id)
                     ?? throw new UserNotFoundException(request.Id);

        return new UserGetResponse
        {
            Id = entity.Id,
            Email = entity.Email,
            PasswordHash = entity.PasswordHash
        };
    }
}