using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Controller.Responses.Users;

namespace DataPoints.Application.Members.Queries.User.GetById;

public record UserGetByIdQuery(Guid Id) : IQuery<UserGetResponse>
{

}