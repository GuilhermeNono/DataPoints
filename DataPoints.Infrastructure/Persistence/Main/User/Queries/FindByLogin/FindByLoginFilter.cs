using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.User.Queries.FindByLogin;

public record FindByLoginFilter(string Login) : IFilter;