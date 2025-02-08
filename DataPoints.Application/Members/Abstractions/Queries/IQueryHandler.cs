using MediatR;

namespace DataPoints.Application.Members.Abstractions.Queries;

public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IQuery<TResponse>
{
}

public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest> where TRequest : IQuery
{
}
