using DataPoints.Api.Jobs.Abstractions.Interfaces;
using DataPoints.Api.Jobs.Recurring;
using Hangfire;

namespace DataPoints.Api.Extensions;

public static class JobExtension
{
    public static void RegisterHangfireJobs(this IServiceCollection services)
    {
        services.AddJob<CheckChainJob>();
    }

    public static void MapHangfireJobs(this IServiceProvider serviceProvider)
    {
        // IRecurringJob é Scoped — resolver direto do provider raiz lançaria em ambientes com
        // ValidateScopes habilitado (padrão em Development).
        using var scope = serviceProvider.CreateScope();

        var recurringJob = scope.ServiceProvider.GetRequiredService<IRecurringJobManagerV2>();

        var recurringJobServices = scope.ServiceProvider.GetServices(typeof(IRecurringJob)).Cast<IRecurringJob>();

        var cancellationToken = new CancellationTokenSource();

        foreach (var jobService in recurringJobServices)
        {
            recurringJob.AddOrUpdate(jobService.GetType().Name,
                () => jobService.ExecuteAsync(cancellationToken.Token),
                jobService.CronExpression);
        }
    }

    private static void AddJob<TJob>(this IServiceCollection service) where TJob : class, IRecurringJob, IJob
    {
        service.AddScoped<IRecurringJob, TJob>();
    }
}
