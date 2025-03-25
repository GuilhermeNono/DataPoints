using DataPoints.Api.Jobs.Abstractions.Interfaces;
using Hangfire;
using MediatR;

namespace DataPoints.Api.Jobs.Recurring;

public class CheckChainJob : IRecurringJob
{
    private readonly ISender _sender;
    
    public string CronExpression => Cron.Minutely();

    public CheckChainJob(ISender sender)
    {
        _sender = sender;
    }
    
    public Task ExecuteAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}