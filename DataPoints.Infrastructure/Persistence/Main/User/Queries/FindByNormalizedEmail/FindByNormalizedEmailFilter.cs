using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.User.Queries.FindByNormalizedEmail;

public record FindByNormalizedEmailFilter(string Login) : IFilter;