using DataPoints.Api.Extensions;
using DataPoints.Api.Middlewares;
using DataPoints.Application;
using DataPoints.Infrastructure;
using DataPoints.Infrastructure.DbUp;
using DataPoints.Presentation.Controllers.Abstractions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
    {
        opt.Filters.Add<ExceptionHandler>();
    })
    .AddApplicationPart(typeof(ApiController).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureServices();

#region || Serilog Configuration ||

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

#endregion

#region || MediatR ||

builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(AssemblyReference.Assembly));

#endregion

#region || Database ||

builder.Services.ConfigureDatabase(builder.Configuration)
    .AddMainRepositories()
    .AddAuditRepositories();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region || DbUp ||

app.RunFunctionsDbUp(builder.Configuration)
    .RunMainDbUp(builder.Configuration)
    .RunAuditDbUp(builder.Configuration);

#endregion

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
