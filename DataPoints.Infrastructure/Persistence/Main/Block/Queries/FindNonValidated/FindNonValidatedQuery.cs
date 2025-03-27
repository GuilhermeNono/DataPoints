using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Block.Queries.FindNonValidated;

public class FindNonValidatedQuery(FindNonValidatedFilter filter) : CustomQuery<FindNonValidatedFilter, BlockEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Blc_Block blc ");
        Add("    where not exists(select 1 ");
        Add("                       from Btc_Checkpoints btc");
        Add($"                     where IdBatch = {Param(x => x.ValidationId)} ");
        Add($"                       and IdBlock = blc.Id)");
    }
}