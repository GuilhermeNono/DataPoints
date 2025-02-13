using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;

namespace DataPoints.Infrastructure.Persistence.Main;

public class WalletRepository : CrudRepository<WalletEntity, Guid>, IWalletRepository
{
    public WalletRepository(MainContext context) : base(context)
    {
    }
}
