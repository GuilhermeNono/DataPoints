using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Users;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.User.Queries.FindByEmail;
using DataPoints.Infrastructure.Persistence.Main.User.Queries.FindByNormalizedEmail;
using Microsoft.AspNetCore.Identity;

namespace DataPoints.Infrastructure.Persistence.Main.User;

public class UserRepository : CrudRepository<UserEntity, Guid>, IUserRepository
{
    private readonly IPersonRepository _personRepository;

    public UserRepository(MainContext context, IPersonRepository personRepository) : base(context)
    {
        _personRepository = personRepository;
    }

    public Task<UserEntity?> FindByNormalizedEmail(string normalizedEmail)
    {
        var query = new FindByNormalizedEmailQuery(new FindByNormalizedEmailFilter(normalizedEmail));

        return QuerySingle(query);
    }

    public Task<UserEntity?> FindByEmail(string email)
    {
        var query = new FindByEmailQuery(new FindByEmailFilter(email));

        return QuerySingle(query);
    }

    public Task<string> GetUserIdAsync(UserEntity user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(user.Id.ToString());
    }

    public Task<string?> GetUserNameAsync(UserEntity user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(user.NormalizedEmail)!;
    }

    public Task SetUserNameAsync(UserEntity user, string? userName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.CompletedTask;
    }

    public async Task<string?> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return (await GetUserNameAsync(user, cancellationToken))?.ToUpper();
    }

    public Task SetNormalizedUserNameAsync(UserEntity user, string? normalizedName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.CompletedTask;
    }

    public async Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        // await Add(user, cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Update(user, cancellationToken);

        return IdentityResult.Success;
    }

    public Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(IdentityResult.Success);
    }

    public async Task<UserEntity?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await FindById(Guid.Parse(userId));
    }

    public Task<UserEntity?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult((UserEntity?)null);
    }

    public async Task SetPasswordHashAsync(UserEntity user, string? passwordHash, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        user.PasswordHash = passwordHash ?? user.PasswordHash;

        await UpdateAsync(user, cancellationToken);
    }

    public Task<string?> GetPasswordHashAsync(UserEntity user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(user.PasswordHash)!;
    }

    public async Task<bool> HasPasswordAsync(UserEntity user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await GetPasswordHashAsync(user, cancellationToken) is not null;
    }

    public async Task SetEmailAsync(UserEntity user, string? email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        user.Email = email ?? user.Email;

        await UpdateAsync(user, cancellationToken);
    }

    public Task<string?> GetEmailAsync(UserEntity user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(user.Email)!;
    }

    public Task<bool> GetEmailConfirmedAsync(UserEntity user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(user.IsEmailConfirmed);
    }

    public async Task SetEmailConfirmedAsync(UserEntity user, bool confirmed, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        user.IsEmailConfirmed = confirmed;

        await UpdateAsync(user, cancellationToken);
    }

    public async Task<UserEntity?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var userEntity = await FindByEmail(normalizedEmail)
                         ?? throw new UserEmailNotFoundException();

        return userEntity;
    }

    public Task<string?> GetNormalizedEmailAsync(UserEntity user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(user.NormalizedEmail)!;
    }

    public Task SetNormalizedEmailAsync(UserEntity user, string? normalizedEmail, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.CompletedTask;
    }

    public async Task SetSecurityStampAsync(UserEntity user, string stamp, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        user.SecurityStamp = stamp;

        await UpdateAsync(user, cancellationToken);
    }

    public Task<string?> GetSecurityStampAsync(UserEntity user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(user.SecurityStamp.ToString())!;
    }
}
