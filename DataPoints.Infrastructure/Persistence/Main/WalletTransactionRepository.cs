using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;

namespace DataPoints.Infrastructure.Persistence.Main;

public class WalletTransactionRepository : CrudRepository<WalletTransactionEntity, Guid>, IWalletTransactionRepository
{
    public WalletTransactionRepository(MainContext context) : base(context)
    {
    }
}
