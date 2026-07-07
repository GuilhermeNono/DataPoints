using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using DataPoints.Api.Configurations;
using DataPoints.Api.Extensions;
using DataPoints.Api.Middlewares;
using DataPoints.Application;
using DataPoints.Application.Members.Behaviours;
using DataPoints.Crosscutting.Configurations;
using DataPoints.Infrastructure;
using DataPoints.Infrastructure.DbUp;
using DataPoints.Infrastructure.EFCore.Database.Context;
using DataPoints.Presentation.Controllers.Abstractions;
using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterConfiguration(builder.Configuration);

builder.Services.AddControllers(opt =>
    {
        opt.Filters.Add<ExceptionHandler>();
    })
    .AddApplicationPart(typeof(ApiController).Assembly)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureServices();

#region || Fluent Validation ||

builder.Services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

#endregion

#region || Serilog Configuration ||

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

#endregion

#region || MediatR ||

builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(AssemblyReference.Assembly));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(IdempotencyPipelineBehaviour<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehaviour<,>));

#endregion

#region || Database ||

builder.Services.ConfigureDatabase(builder.Configuration)
    .AddMainRepositories()
    .AddAuditRepositories();

#endregion

#region || Identity ||

var jwtConfiguration = ConfigurationExtension.GetConfiguration<JwtConfiguration, IJwtConfiguration>(builder.Configuration);

builder.Services.ConfigureIdentity(jwtConfiguration);
builder.Services.ConfigureAuthentication(jwtConfiguration);
builder.Services.ConfigureAuthorization();

#endregion

#region || HangFire ||

builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(opt => opt.UseNpgsqlConnection(
        builder.Configuration.GetConnectionString(DataPoints.Infrastructure.ServiceExtensions.MainConnectionName)),
        new PostgreSqlStorageOptions { SchemaName = "hangfire" }));

builder.Services.AddHangfireServer();

builder.Services.RegisterHangfireJobs();

#endregion

#region || Rate Limiting (SEC-4) ||

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddFixedWindowLimiter("auth", limiter =>
    {
        limiter.PermitLimit = 10;
        limiter.Window = TimeSpan.FromMinutes(1);
        limiter.QueueLimit = 0;
    });

    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));
});

#endregion

#region || Health Checks (USE-2) ||

builder.Services.AddHealthChecks()
    .AddDbContextCheck<MainContext>("database", tags: ["ready"]);

#endregion

var app = builder.Build();

app.Services.MapHangfireJobs();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}

#region || DbUp ||

app.RunFunctionsDbUp(builder.Configuration)
    .RunMainDbUp(builder.Configuration)
    .RunAuditDbUp(builder.Configuration);

#endregion

app.UseHttpsRedirection();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseSerilogRequestLogging();
app.UseRouting();
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions { Predicate = _ => false });
app.MapHealthChecks("/ready", new HealthCheckOptions { Predicate = check => check.Tags.Contains("ready") });

app.Run();
