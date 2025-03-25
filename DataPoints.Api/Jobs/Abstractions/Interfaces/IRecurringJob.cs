namespace DataPoints.Api.Jobs.Abstractions.Interfaces;

public interface IRecurringJob : IJob
{
    public string CronExpression { get; }
}