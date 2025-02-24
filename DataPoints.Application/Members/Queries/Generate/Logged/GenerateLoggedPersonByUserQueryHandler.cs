using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Person;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Objects;
using DataPoints.Domain.Repositories.Audit;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Queries.Generate.Logged;

public class GenerateLoggedPersonByUserQueryHandler : IQueryHandler<GenerateLoggedPersonByUserQuery, LoggedPerson>
{
    private readonly IUserRepository _userRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IPermissionLogRepository _permissionLogRepository;
    private readonly IProfileRepository _profileRepository;

    public GenerateLoggedPersonByUserQueryHandler(IPermissionLogRepository permissionLogRepository,
        IPersonRepository personRepository, IUserRepository userRepository, IProfileRepository profileRepository)
    {
        _permissionLogRepository = permissionLogRepository;
        _personRepository = personRepository;
        _userRepository = userRepository;
        _profileRepository = profileRepository;
    }

    public async Task<LoggedPerson> Handle(GenerateLoggedPersonByUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindById(request.UserId)
                   ?? throw new UserNotFoundException(request.UserId);

        var person = await _personRepository.FindById(request.UserId)
            ?? throw new PersonNotFoundException();

        var roles = (await _profileRepository.FindRolesByUser(user.Id)).Select(x => x.Name);

        return new LoggedPerson()
        {
            Id = user.Id,
            Name = person.FullName,
            Roles = roles
        };
    }
}