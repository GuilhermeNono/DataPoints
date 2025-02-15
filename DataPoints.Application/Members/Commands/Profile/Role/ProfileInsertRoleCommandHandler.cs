using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Crosscutting.Exceptions.Http.Conflict;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Permissions;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;
using DataPoints.Domain.Entities.Audit;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Audit;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Commands.Profile.Role;

public class ProfileInsertRoleCommandHandler : ICommandHandler<ProfileInsertRoleCommand>
{
    
    private readonly IProfileRepository _profileRepository;
    private readonly IProfileLogRepository _profileLogRepository;
    
    private readonly IPermissionRepository _permissionRepository;
    
    private readonly IUserRepository _userRepository;

    public ProfileInsertRoleCommandHandler(IProfileRepository profileRepository, IProfileLogRepository profileLogRepository, IPermissionRepository permissionRepository, IUserRepository userRepository)
    {
        _profileRepository = profileRepository;
        _profileLogRepository = profileLogRepository;
        _permissionRepository = permissionRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(ProfileInsertRoleCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.Exists(request.IdUser))
            throw new UserNotFoundException(request.IdUser);
        
        var profile = await _profileRepository.FindByRole(request.IdUser, request.Role);

        if (profile is not null)
            throw new UserAlreadyHasThatRoleException(request.Role);

        var role = await _permissionRepository.FindByName(request.Role)
                   ?? throw new PermissionRoleNotFoundException(request.Role);

        var newProfile = new ProfileEntity()
        {
            IdUser = request.IdUser,
            IdPermission = role.Id
        };

        await _profileRepository.Add(newProfile, request.LoggedPerson.Name, cancellationToken);
        await _profileLogRepository.Add(new ProfileLogEntity(newProfile), request.LoggedPerson.Name, cancellationToken);
    }
}
