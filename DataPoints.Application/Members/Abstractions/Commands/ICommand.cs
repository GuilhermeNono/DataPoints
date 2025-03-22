using DataPoints.Domain.Database.Transaction;
using MediatR;

namespace DataPoints.Application.Members.Abstractions.Commands;

public interface ICommand<out T> : IRequest<T>, ITransactional
{
}

public interface ICommand : IRequest, ITransactional
{
}
