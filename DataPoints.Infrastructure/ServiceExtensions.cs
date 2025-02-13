using DataPoints.Domain.Database.Context;
using DataPoints.Domain.Database.Transaction;
using DataPoints.Domain.Repositories.Audit;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.EFCore.Database.Services;
using DataPoints.Infrastructure.Persistence.Audit;
using DataPoints.Infrastructure.Persistence.Main;
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

    public static IServiceCollection AddMainRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IPersonRepository , PersonRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IUserRepository , UserRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();

        return services;
    }    
    
    public static IServiceCollection AddAuditRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPermissionLogRepository, PermissionLogRepository>();
        services.AddScoped<IPersonLogRepository , PersonLogRepository>();
        services.AddScoped<IProfileLogRepository, ProfileLogRepository>();
        services.AddScoped<IUserLogRepository , UserLogRepository>();
        services.AddScoped<IWalletLogRepository, WalletLogRepository>();

        return services;
    }
}
