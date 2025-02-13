using DataPoints.Application;
using DataPoints.Infrastructure;
using DataPoints.Infrastructure.DbUp;
using DataPoints.Presentation.Controllers.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(ApiController).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
