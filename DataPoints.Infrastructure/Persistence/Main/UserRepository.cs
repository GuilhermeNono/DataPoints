using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;

namespace DataPoints.Infrastructure.Persistence.Main;

public class UserRepository : CrudRepository<UserEntity, Guid>, IUserRepository
{
    public UserRepository(MainContext context) : base(context)
    {
    }
}
