using System.Text;
using DataPoints.Crosscutting.Exceptions.Http.Internal;
using DataPoints.Domain.Helpers;
using DbUp;
using DbUp.Engine;
using DbUp.Postgresql;
using DbUp.ScriptProviders;
using DbUp.Support;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace DataPoints.Infrastructure.DbUp;

public static class DbUpExtension
{
    public static IApplicationBuilder RunFunctionsDbUp(this IApplicationBuilder application,
        IConfiguration configuration)
    {
        Console.WriteLine("| Checando funções customizadas do Banco |");

        var connectionString = configuration.GetConnectionString(ServiceExtensions.MainConnectionName);

        EnsureDatabase.For.PostgresqlDatabase(connectionString);

        var upgrader = ConfigureEngine(connectionString!, "migrations_functions", "Postgres", "Functions");

        var result = upgrader.PerformUpgrade();
        if (!result.Successful)
            throw new DatabaseMigrationFailed();

        Console.WriteLine("| Checagem das funções finalizada |\n");

        return application;
    }

    public static IApplicationBuilder RunMainDbUp(this IApplicationBuilder application, IConfiguration configuration)
    {
        Console.WriteLine("| Checando arquivos de migração do schema core |");
        var connectionString = configuration.GetConnectionString(ServiceExtensions.MainConnectionName);

        EnsureDatabase.For.PostgresqlDatabase(connectionString);

        var upgrader = ConfigureEngine(connectionString!, "migrations_core", "Postgres", "Core");

        var result = upgrader.PerformUpgrade();
        if (!result.Successful)
            throw new DatabaseMigrationFailed();
        Console.WriteLine("| Checagem de migração do schema core finalizada |\n");

        return application;
    }

    public static IApplicationBuilder RunAuditDbUp(this IApplicationBuilder application, IConfiguration configuration)
    {
        Console.WriteLine("| Checando arquivos de migração do schema audit |");
        var connectionString = configuration.GetConnectionString(ServiceExtensions.MainConnectionName);

        EnsureDatabase.For.PostgresqlDatabase(connectionString);

        var upgrader = ConfigureEngine(connectionString!, "migrations_audit", "Postgres", "Audit");

        var result = upgrader.PerformUpgrade();
        if (!result.Successful)
            throw new DatabaseMigrationFailed();
        Console.WriteLine("| Checagem de migração do schema audit finalizada |\n");

        return application;
    }

    private static UpgradeEngine ConfigureEngine(string connectionString, string journalTableName, params string[] folder) => DeployChanges.To
        .PostgresqlDatabase(connectionString)
        .WithScriptsFromFileSystem(Path.Combine([AppDomain.CurrentDomain.BaseDirectory, "DbUp", "Scripts", .. folder]),
            new FileSystemScriptOptions
            {
                IncludeSubDirectories = true,
                Extensions = ["*.sql"],
                Encoding = Encoding.UTF8
            })
        .JournalToPostgresqlTable("public", journalTableName)
        .WithFilter(new ExecutionOrderScriptFilter())
        .WithTransactionPerScript()
        .Build();
}

public class ExecutionOrderScriptFilter : IScriptFilter
{
    private const string ScriptFileName = "script";
    private const string TempScriptFileName = "Temp";

    public IEnumerable<SqlScript> Filter(
        IEnumerable<SqlScript> sorted,
        HashSet<string> executedScriptNames,
        ScriptNameComparer comparer)
    {
        return sorted
            .Where(s => s.SqlScriptOptions.ScriptType == ScriptType.RunAlways ||
                        !executedScriptNames.Contains(s.Name, comparer)).Where(x =>
                !EnvironmentHelper.IsProductionEnvironment || x.Name.Contains(ScriptFileName) ||
                (!x.Name.Contains(ScriptFileName) && !x.Name.Contains(TempScriptFileName)))
            .OrderByDescending(str => str.Name.Contains(ScriptFileName));
    }
}
