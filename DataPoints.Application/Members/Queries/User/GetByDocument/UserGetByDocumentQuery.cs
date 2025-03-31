using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Domain.CustomQuery;

namespace DataPoints.Application.Members.Queries.User.GetByDocument;

public record UserGetByDocumentQuery(string Document) : IQuery<UserInfoQueryResponse>
{
    
}