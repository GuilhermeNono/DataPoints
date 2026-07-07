namespace DataPoints.Application.Members.Abstractions.Commands;

public interface IIdempotentCommand
{
    Guid IdempotencyKey { get; }
}
