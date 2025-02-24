using System.Text;
using DataPoints.Crosscutting.Configurations;
using DataPoints.Domain.Database.Context;
using DataPoints.Domain.Database.Transaction;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Helpers;
using DataPoints.Domain.Repositories.Audit;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Infrastructure.EFCore.Database.Services;
using DataPoints.Infrastructure.Persistence.Audit;
using DataPoints.Infrastructure.Persistence.Main.Permission;
using DataPoints.Infrastructure.Persistence.Main.Person;
using DataPoints.Infrastructure.Persistence.Main.Profile;
using DataPoints.Infrastructure.Persistence.Main.Token.Refresh;
using DataPoints.Infrastructure.Persistence.Main.User;
using DataPoints.Infrastructure.Persistence.Main.Wallet;
using DataPoints.Infrastructure.Persistence.Main.Wallet.Transaction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DataPoints.Infrastructure;

public static class ServiceExtensions
{
    private const string MainConnectionName = "MainDatabase";
    private const string AuditConnectionName = "AuditDatabase";

    #region || Database ||

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
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }

    public static IServiceCollection AddAuditRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPermissionLogRepository, PermissionLogRepository>();
        services.AddScoped<IPersonLogRepository, PersonLogRepository>();
        services.AddScoped<IProfileLogRepository, ProfileLogRepository>();
        services.AddScoped<IUserLogRepository, UserLogRepository>();
        services.AddScoped<IWalletLogRepository, WalletLogRepository>();

        return services;
    }

    #endregion

    #region || Auth ||

    public static void ConfigureAuthentication(this IServiceCollection services)
    {
        var jwtConfiguration = services.BuildServiceProvider().GetRequiredService<IJwtConfiguration>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey))
                };
            });
    }

    public static void ConfigureAuthorization
        (this IServiceCollection services)
    {
        services.AddAuthorization(opt =>
        {
            opt.AddPolicy(RoleHelper.Administrator, policy => { policy.RequireRole(RoleHelper.Administrator); });
            opt.AddPolicy(RoleHelper.User, policy => { policy.RequireRole(RoleHelper.User); });
        });
    }

    #endregion


    #region || Identity ||

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromMinutes(3));

        services.AddIdentity<UserEntity, PermissionEntity>(o =>
            {
                o.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                o.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddUserStore<UserRepository>()
            .AddDefaultTokenProviders()
            .AddRoleStore<PermissionRepository>()
            .AddSignInManager<SignInManager<UserEntity>>();

        services.AddScoped<UserManager<UserEntity>>();

        services.AddHttpContextAccessor();
    }

    #endregion
}