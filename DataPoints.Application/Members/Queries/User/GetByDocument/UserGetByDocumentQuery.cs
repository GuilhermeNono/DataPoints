using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Domain.CustomQuery;
using DataPoints.Domain.Objects;

namespace DataPoints.Application.Members.Queries.User.GetByDocument;

public record UserGetByDocumentQuery(string Document, LoggedPerson LoggedPerson) : IQuery<UserInfoQueryResponse>
{

}