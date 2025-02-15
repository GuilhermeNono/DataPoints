using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Entities.Main;
using Microsoft.AspNetCore.Identity;

namespace DataPoints.Domain.Repositories.Main;

public interface IUserRepository : ICrudRepository<UserEntity, Guid>, IUserPasswordStore<UserEntity>, IUserEmailStore<UserEntity>,
    IUserSecurityStampStore<UserEntity>
{
    Task<UserEntity?> FindByNormalizedEmail(string normalizedEmail);
}
