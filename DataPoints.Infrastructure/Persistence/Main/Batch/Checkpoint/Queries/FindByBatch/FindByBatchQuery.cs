using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Batch.Checkpoint.Queries.FindByBatch;

public class FindByBatchQuery(FindByBatchFilter filter) : CustomQuery<FindByBatchFilter, BatchCheckpointEntity>(filter)
{
    private readonly FindByBatchFilter _filter = filter;

    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM core.btc_checkpoints ");
        Add($"    WHERE idbatch = {Param(x => x.ValidationId)} ");
        Add($"      AND isvalid = {Param(x => x.OnlyValidated)} ", !_filter.ShowInvalidatedOnes);
    }
}