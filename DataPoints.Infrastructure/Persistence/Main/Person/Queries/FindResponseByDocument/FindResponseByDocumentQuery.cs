using DataPoints.Domain.CustomQuery;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery;

namespace DataPoints.Infrastructure.Persistence.Main.Person.Queries.FindResponseByDocument;

public class FindResponseByDocumentQuery(FindResponseByDocumentFilter filter) : CustomQuery<FindResponseByDocumentFilter, UserInfoQueryResponse>(filter)
{
    protected override void Prepare()
    {
        Add($"   SELECT ppl.DocumentNumber {Alias(x => x.FullDocument)}, ");
        Add($"          ppl.FirstName {Alias(x => x.FirstName)}, ");
        Add($"          ppl.LastName {Alias(x => x.LastName)}, ");
        Add($"          wlt.Hash {Alias(x => x.WalletCode)}, ");
        Add($"          ppl.Id {Alias(x => x.Id)} ");
        Add("     FROM ppl_people ppl ");
        Add("    INNER JOIN wlt_wallets wlt on wlt.IdUser = ppl.Id ");
    }
}