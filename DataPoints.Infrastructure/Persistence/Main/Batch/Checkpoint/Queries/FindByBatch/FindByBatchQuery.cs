using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Batch.Checkpoint.Queries.FindByBatch;

public class FindByBatchQuery(FindByBatchFilter filter) : CustomQuery<FindByBatchFilter, BatchCheckpointEntity>(filter)
{
    private readonly FindByBatchFilter _filter = filter;

    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Btc_Checkpoints ");
        Add($"    WHERE IdBatch = {Param(x => x.ValidationId)} ");
        Add($"      AND IsValid = {Param(x => x.OnlyValidated)} ", !_filter.ShowInvalidatedOnes);
    }
}