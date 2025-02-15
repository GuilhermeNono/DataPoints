using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.User.Queries.FindByEmail;

public record FindByEmailFilter(string Login) : IFilter;
