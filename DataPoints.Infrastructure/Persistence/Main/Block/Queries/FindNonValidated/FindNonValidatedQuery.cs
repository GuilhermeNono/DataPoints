using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Block.Queries.FindNonValidated;

public class FindNonValidatedQuery(FindNonValidatedFilter filter) : CustomQuery<FindNonValidatedFilter, BlockEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM core.blc_block blc ");
        Add("    where not exists(select 1 ");
        Add("                       from core.btc_checkpoints btc");
        Add($"                     where idbatch = {Param(x => x.ValidationId)} ");
        Add($"                       and idblock = blc.id)");
        Add($"     and isvalid = {Param(x => x.IsValid)} ");
    }
}