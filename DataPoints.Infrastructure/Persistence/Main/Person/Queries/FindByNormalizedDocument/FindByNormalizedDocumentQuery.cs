using DataPoints.Domain.Entities.Main;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Person.Queries.FindByNormalizedDocument;

public class FindByNormalizedDocumentQuery(FindByNormalizedDocumentFilter filter) : CustomQuery<FindByNormalizedDocumentFilter, PersonEntity>(filter)
{
    protected override void Prepare()
    {
        Add("   SELECT * ");
        Add("     FROM core.ppl_people ");
        Add($"   WHERE normalizeddocumentnumber = {Param(x => x.Document)} ");
    }
}