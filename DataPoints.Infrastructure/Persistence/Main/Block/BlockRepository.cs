using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Abstractions;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.Persistence.Main.Block.Queries.FindLastBlock;

namespace DataPoints.Infrastructure.Persistence.Main.Block;

public class BlockRepository : CrudRepository<BlockEntity, Guid>, IBlockRepository
{
    public BlockRepository(MainContext context) : base(context)
    {
    }

    public Task<BlockEntity?> FindLastBlock()
    {
        var query = new FindLastBlockQuery()
            .OrderBy(x => x.DateInclusion, Sort.Desc);

        return QuerySingle(query);
    }
}