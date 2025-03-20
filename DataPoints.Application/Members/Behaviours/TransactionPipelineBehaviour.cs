using System.Reflection;
using DataPoints.Domain.Annotations;
using DataPoints.Domain.Database.Transaction;
using DataPoints.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DataPoints.Application.Members.Behaviours;

public class TransactionPipelineBehaviour<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactional
{
    private readonly ILogger<TransactionPipelineBehaviour<TRequest, TResponse>> _logger;
    private readonly ITransactionService _transactionService;

    public TransactionPipelineBehaviour(ILogger<TransactionPipelineBehaviour<TRequest, TResponse>> logger,
        ITransactionService transactionService)
    {
        _logger = logger;
        _transactionService = transactionService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse? response = default;

        var transactionAnnotation = request.GetType().GetCustomAttribute<TransactionTypeAttribute>();

        if (transactionAnnotation?.TransactionType is DbTransactionType.NoTransaction)
            return await next();

        var transactionType = transactionAnnotation?.TransactionType ?? DbTransactionType.ReadUncommitted;

        try
        {
            await _transactionService.ExecuteInTransactionContextAsync(async () => { response = await next(); },
                transactionType, cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError("|> Ocorreu um problema durante o processo de transação:\n {Message} \n {StackTracer}",
                e.Message, e.StackTrace);
            throw;
        }

        return response!;
    }
}