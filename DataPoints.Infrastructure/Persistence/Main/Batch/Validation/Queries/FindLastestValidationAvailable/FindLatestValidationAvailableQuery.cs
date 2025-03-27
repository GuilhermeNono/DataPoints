using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Batch.Validation.Queries.FindLastestValidationAvailable;

public class FindLatestValidationAvailableQuery(FindLatestValidationAvailableFilter filter)
    : CustomQuery<FindLatestValidationAvailableFilter, BatchValidationEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Btc_Validations ");
        Add($"    WHERE IdBatchStatus = {Param(x => x.ProcessingState)} ");
        Add($"       OR IdBatchStatus = {Param(x => x.PendingState)} ");
    }
}