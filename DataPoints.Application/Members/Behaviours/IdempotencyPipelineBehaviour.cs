using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Crosscutting.Exceptions.Http.Conflict;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Enums.Entities;
using DataPoints.Domain.Repositories.Main;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DataPoints.Application.Members.Behaviours;

public class IdempotencyPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IIdempotencyKeyRepository _repository;
    private readonly ILogger<IdempotencyPipelineBehaviour<TRequest, TResponse>> _logger;

    public IdempotencyPipelineBehaviour(IIdempotencyKeyRepository repository,
        ILogger<IdempotencyPipelineBehaviour<TRequest, TResponse>> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not IIdempotentCommand idempotentCommand)
            return await next();

        var requestHash = ComputeHash(request);

        var existing = await _repository.FindById(idempotentCommand.IdempotencyKey);

        if (existing is not null)
        {
            if (existing.RequestHash != requestHash)
                throw new IdempotencyKeyConflictException();

            if (existing.Status is IdempotencyStatus.Completed && existing.ResponseBody is not null)
                return JsonSerializer.Deserialize<TResponse>(existing.ResponseBody)!;

            throw new IdempotencyKeyConflictException();
        }

        var entry = new IdempotencyKeyEntity
        {
            Id = idempotentCommand.IdempotencyKey,
            RequestHash = requestHash,
        };

        try
        {
            await _repository.Add(entry, "system", cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "|> Idempotency-Key {Key} já está sendo processada", idempotentCommand.IdempotencyKey);
            throw new IdempotencyKeyConflictException();
        }

        try
        {
            var response = await next();

            entry.Status = IdempotencyStatus.Completed;
            entry.ResponseBody = JsonSerializer.Serialize(response);
            await _repository.Update(entry, "system", cancellationToken);

            return response;
        }
        catch
        {
            await _repository.Delete(entry, cancellationToken);
            throw;
        }
    }

    private static string ComputeHash(TRequest request)
    {
        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));
        return Convert.ToBase64String(SHA256.HashData(bytes));
    }
}
