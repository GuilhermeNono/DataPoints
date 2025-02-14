using DataPoints.Api.Configurations;
using DataPoints.Crosscutting.Configurations;

namespace DataPoints.Api.Extensions;

public static class ConfigurationExtension
{
    private static void AddConfiguration<TService, TIService>(this IServiceCollection services,
        IConfiguration configuration)
        where TService : class where TIService : class
    {
        services.Configure<TService>(configuration.GetSection(nameof(TService)));
        services.AddSingleton(GetConfiguration<TService, TIService>(configuration));
    }

    public static TIService GetConfiguration<TService, TIService>(IConfiguration configuration)
    {
        var instance = (TIService)Activator.CreateInstance(typeof(TService))!;
        configuration.GetSection(instance.GetType().Name).Bind(instance);

        return instance;
    }

    public static void RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfiguration<JwtConfiguration, IJwtConfiguration>(configuration);
    }
}