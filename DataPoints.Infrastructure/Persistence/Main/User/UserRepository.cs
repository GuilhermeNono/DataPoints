using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.User.Queries.FindByLogin;

namespace DataPoints.Infrastructure.Persistence.Main.User;

public class UserRepository : CrudRepository<UserEntity, Guid>, IUserRepository
{
    public UserRepository(MainContext context) : base(context)
    {
    }

    public Task<UserEntity?> FindByLogin(string login)
    {
        var query = new FindByLoginQuery(new FindByLoginFilter(login));

        return QuerySingle(query);
    }
}
