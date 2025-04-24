using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Person.Queries.FindByNormalizedDocument;

public record FindByNormalizedDocumentFilter(string Document) : IFilter;