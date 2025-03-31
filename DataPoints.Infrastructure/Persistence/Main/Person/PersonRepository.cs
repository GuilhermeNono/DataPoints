using DataPoints.Contract.Users.Responses;
using DataPoints.Domain.CustomQuery;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.Person.Queries.FindByDocument;
using DataPoints.Infrastructure.Persistence.Main.Person.Queries.FindResponseByDocument;

namespace DataPoints.Infrastructure.Persistence.Main.Person;

public class PersonRepository : CrudRepository<PersonEntity, Guid>, IPersonRepository
{
    public PersonRepository(MainContext context) : base(context)
    {
    }

    public Task<PersonEntity?> FindByDocument(string documentNormalized)
    {
        var query = new FindByDocumentQuery(new FindByDocumentFilter(documentNormalized));

        return QuerySingle(query);
    }   
    
    public Task<UserInfoQueryResponse?> FindResponseByDocument(string documentNormalized)
    {
        var query = new FindResponseByDocumentQuery(new FindResponseByDocumentFilter(documentNormalized));

        return QuerySingle(query);
    }
}
