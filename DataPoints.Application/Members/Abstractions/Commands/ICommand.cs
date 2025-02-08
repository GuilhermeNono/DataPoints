using MediatR;

namespace DataPoints.Application.Members.Abstractions.Commands;

public interface ICommand<out T> : IRequest<T>
{
}

public interface ICommand : IRequest
{
}
