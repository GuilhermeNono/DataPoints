using MediatR;

namespace DataPoints.Application.Members.Abstractions.Queries;

public interface IQuery<out T> : IRequest<T>
{
}

public interface IQuery : IRequest
{
}
