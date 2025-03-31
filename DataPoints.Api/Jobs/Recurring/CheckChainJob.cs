using DataPoints.Api.Jobs.Abstractions.Interfaces;
using DataPoints.Application.Members.Commands.Block.Checkpoint;
using DataPoints.Domain.Objects;
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
    
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await _sender.Send(new BlockValidateCheckpointCommand(LoggedPerson.System()), cancellationToken);
    }
}