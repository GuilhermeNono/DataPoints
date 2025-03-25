namespace DataPoints.Api.Jobs.Abstractions.Interfaces;

public interface IJob
{
    public Task ExecuteAsync(CancellationToken cancellationToken);
}