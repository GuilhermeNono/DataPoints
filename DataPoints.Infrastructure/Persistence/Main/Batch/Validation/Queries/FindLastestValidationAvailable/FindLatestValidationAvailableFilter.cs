using DataPoints.Domain.Database.Queries.Base;
using DataPoints.Domain.Enums.Entities;

namespace DataPoints.Infrastructure.Persistence.Main.Batch.Validation.Queries.FindLastestValidationAvailable;

public record FindLatestValidationAvailableFilter : IFilter
{
    public BatchStateType PendingState => BatchStateType.Pending;
    public BatchStateType ProcessingState => BatchStateType.Processing;
}