using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Queries.Generate.Logged;

public record GenerateLoggedPersonByUserQuery(Guid UserId) : IQuery<LoggedPerson>
{
    
}