using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Batch.Validation.Queries.FindLastestValidationAvailable;

public class FindLatestValidationAvailableQuery(FindLatestValidationAvailableFilter filter)
    : CustomQuery<FindLatestValidationAvailableFilter, BatchValidationEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM core.btc_validations ");
        Add($"    WHERE idbatchstatus = {Param(x => x.ProcessingState)} ");
        Add($"       OR idbatchstatus = {Param(x => x.PendingState)} ");
    }
}