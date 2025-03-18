using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;

namespace DataPoints.Infrastructure.Persistence.Main.Block;

public class BlockRepository : CrudRepository<BlockEntity, Guid>, IBlockRepository
{
    public BlockRepository(MainContext context) : base(context)
    {
    }
}