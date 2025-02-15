using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Person.Queries.FindByDocument;

public record FindByDocumentFilter(string Document): IFilter
{
    
}
