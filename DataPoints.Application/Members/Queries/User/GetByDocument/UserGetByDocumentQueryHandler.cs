using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Person;
using DataPoints.Domain.CustomQuery;
using DataPoints.Domain.Helpers;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Queries.User.GetByDocument;

public class UserGetByDocumentQueryHandler : IQueryHandler<UserGetByDocumentQuery, UserInfoQueryResponse>
{
    private readonly IPersonRepository _personRepository;

    public UserGetByDocumentQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<UserInfoQueryResponse> Handle(UserGetByDocumentQuery request, CancellationToken cancellationToken)
    {
        var document = DocumentHelper.ConfigureHelper(request.Document.Trim());
        
        return await _personRepository.FindResponseByDocument(document.Normalized)
                     ?? throw new PersonNotFoundException();
    }
}