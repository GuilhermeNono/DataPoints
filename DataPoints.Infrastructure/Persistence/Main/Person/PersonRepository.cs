using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;

namespace DataPoints.Infrastructure.Persistence.Main.Person;

public class PersonRepository : CrudRepository<PersonEntity, Guid>, IPersonRepository
{
    public PersonRepository(MainContext context) : base(context)
    {
    }
}
