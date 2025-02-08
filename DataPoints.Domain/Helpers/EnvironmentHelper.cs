namespace DataPoints.Domain.Helpers;

public static class EnvironmentHelper
{
    private const string ProductionEnvironment = "Production";
    private const string StagingEnvironment = "Staging";
    private const string DevelopmentEnvironment = "Development";

    private const string EnvironmentVariable = "ASPNETCORE_ENVIRONMENT";

    public static bool IsProductionEnvironment =>
        Environment.GetEnvironmentVariable(EnvironmentVariable) is ProductionEnvironment;

    public static bool IsStagingEnvironment =>
        Environment.GetEnvironmentVariable(EnvironmentVariable) is StagingEnvironment;

    public static bool IsDevelopmentEnvironment =>
        Environment.GetEnvironmentVariable(EnvironmentVariable) is DevelopmentEnvironment;
}
