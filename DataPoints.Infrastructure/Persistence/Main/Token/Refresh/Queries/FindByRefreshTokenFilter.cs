using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Token.Refresh.Queries;

public record FindByRefreshTokenFilter(string RefreshToken) : IFilter;