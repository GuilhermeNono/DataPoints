using DataPoints.Application.Error.Catcher;
using DataPoints.Domain.Errors.Abstractions.Interfaces;

namespace DataPoints.Api.Extensions;

public static class DependencyInjectionExtension
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IErrorCatcher, ErrorCatcher>();
    }
}