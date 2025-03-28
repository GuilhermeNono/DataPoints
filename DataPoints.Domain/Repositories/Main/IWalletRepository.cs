using DataPoints.Domain.Database.Repository;
using DataPoints.Domain.Entities.Main;

namespace DataPoints.Domain.Repositories.Main;

public interface IWalletRepository : ICrudRepository<WalletEntity, Guid>
{
    Task<WalletEntity?> FindByPublicKey(string publicKey);
    Task<WalletEntity?> FindByHash(string hash);
    Task<WalletEntity?> FindByUser(Guid userId);
}
