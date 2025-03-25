using DataPoints.Api.Extensions;
using DataPoints.Api.Middlewares;
using DataPoints.Application;
using DataPoints.Application.Members.Behaviours;
using DataPoints.Infrastructure;
using DataPoints.Infrastructure.DbUp;
using DataPoints.Presentation.Controllers.Abstractions;
using FluentValidation;
using Hangfire;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterConfiguration(builder.Configuration);

builder.Services.AddControllers(opt =>
    {
        opt.Filters.Add<ExceptionHandler>();
    })
    .AddApplicationPart(typeof(ApiController).Assembly);

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
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehaviour<,>));

#endregion

#region || Database ||

builder.Services.ConfigureDatabase(builder.Configuration)
    .AddMainRepositories()
    .AddAuditRepositories();

#endregion

#region || Identity ||

builder.Services.ConfigureIdentity();
builder.Services.ConfigureAuthentication();
builder.Services.ConfigureAuthorization();

#endregion

#region || HangFire ||

builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFire")));

builder.Services.AddHangfireServer();

builder.Services.RegisterHangfireJobs();
#endregion

var app = builder.Build();

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
app.UseSerilogRequestLogging();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
