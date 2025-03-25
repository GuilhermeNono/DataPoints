using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Block.Queries.FindLastBlock;

public class FindLastBlockQuery : CustomQuery<BlockEntity>
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Blc_Block ");
        Add("    WHERE IsValid = 1 ");
    }
}