using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Controller.Authentication.Me.Response;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Queries.Authentication.Me;

public record MeQuery(LoggedPerson LoggedPerson) : IQuery<MeResponse>
{
    
}