using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Authentication.Me.Response;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Person;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Queries.Authentication.Me;

public class MeQueryHandler : IQueryHandler<MeQuery, MeResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUserRepository _userRepository;

    public MeQueryHandler(IPersonRepository personRepository, IUserRepository userRepository)
    {
        _personRepository = personRepository;
        _userRepository = userRepository;
    }

    public async Task<MeResponse> Handle(MeQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.FindById(request.LoggedPerson.Id.GetValueOrDefault())
                     ?? throw new PersonNotFoundException();
        
        var user = await _userRepository.FindById(request.LoggedPerson.Id.GetValueOrDefault())
            ?? throw new UserNotFoundException(request.LoggedPerson.Id.GetValueOrDefault());

        return new MeResponse(person.Id, person.FirstName, person.LastName, user.Email);
    }
}