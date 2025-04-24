using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Person.Queries.FindByNormalizedDocument;

public class FindByNormalizedDocumentQuery(FindByNormalizedDocumentFilter filter) : CustomQuery<FindByNormalizedDocumentFilter, PersonEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM Ppl_People ");
        Add($"   WHERE NormalizedDocumentNumber = {Param(x => x.Document)} ");
    }
}