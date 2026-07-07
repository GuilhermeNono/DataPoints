using DataPoints.Domain.CustomQuery;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Person.Queries.FindResponseByDocument;

public class FindResponseByDocumentQuery(FindResponseByDocumentFilter filter) : CustomQuery<FindResponseByDocumentFilter, UserInfoQueryResponse>(filter)
{
    protected override void Prepare()
    {
        Add($"   SELECT ppl.documentnumber {Alias(x => x.FullDocument)}, ");
        Add($"          ppl.firstname {Alias(x => x.FirstName)}, ");
        Add($"          ppl.lastname {Alias(x => x.LastName)}, ");
        Add($"          wlt.hash {Alias(x => x.WalletCode)}, ");
        Add($"          ppl.id {Alias(x => x.Id)} ");
        Add("     FROM core.ppl_people ppl ");
        Add("    INNER JOIN core.wlt_wallets wlt on wlt.iduser = ppl.id ");
    }
}