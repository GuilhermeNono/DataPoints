using DataPoints.Domain.Database.Queries.Base;

namespace DataPoints.Infrastructure.Persistence.Main.Profile.Queries.FindRolesByUser;

public record FindRolesByUserFilter(Guid IdUser) : IFilter;