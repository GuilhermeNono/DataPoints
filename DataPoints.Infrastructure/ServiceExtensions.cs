using DataPoints.Domain.Database.Context;
using DataPoints.Domain.Database.Transaction;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.EFCore.Database.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataPoints.Infrastructure;

public static class ServiceExtensions
{
    private const string MainConnectionName = "MainDatabase";
    private const string AuditConnectionName = "AuditDatabase";

    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IMainContext, MainContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString(MainConnectionName)));

        services.AddDbContext<IAuditContext, AuditContext>(opt =>
            {
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                opt.UseSqlServer(configuration.GetConnectionString(AuditConnectionName));
            }
        );

        services.AddScoped<ITransactionService, TransactionService>();

        return services;
    }

}
