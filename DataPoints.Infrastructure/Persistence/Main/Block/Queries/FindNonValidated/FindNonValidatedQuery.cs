using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Block.Queries.FindNonValidated;

public class FindNonValidatedQuery : CustomQuery<BlockEntity>
{
    protected override void Prepare()
    {
        throw new NotImplementedException();
    }
}