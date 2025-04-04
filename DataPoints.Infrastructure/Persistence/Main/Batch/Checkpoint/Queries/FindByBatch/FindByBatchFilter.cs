﻿using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Batch.Checkpoint.Queries.FindByBatch;

public record FindByBatchFilter(Guid ValidationId, bool ShowInvalidatedOnes) : IFilter
{
    public bool OnlyValidated => true;
}