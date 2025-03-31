using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Person.Queries.FindResponseByDocument;

public record FindResponseByDocumentFilter(string DocumentNumber) : IFilter;