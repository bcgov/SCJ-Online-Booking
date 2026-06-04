using Microsoft.Extensions.Configuration;
using Npgsql;

namespace SCJ.Booking.Data.Configuration;

public static class ConnectionStringResolver
{
    private static readonly (string BuilderKey, string EnvVar)[] PartMap =
    [
        ("Host", "DATABASE_HOST"),
        ("Port", "DATABASE_PORT"),
        ("Database", "DATABASE_NAME"),
        ("Username", "DATABASE_USERNAME"),
        ("Password", "DATABASE_PASSWORD"),
    ];

    public static bool HasEnvironmentConnectionOverride()
    {
        if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ConnectionString")))
        {
            return true;
        }

        return PartMap.Any(x =>
            !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(x.EnvVar))
        );
    }

    public static string Resolve(IConfiguration configuration)
    {
        var envConnectionString = Environment.GetEnvironmentVariable("ConnectionString");
        if (!string.IsNullOrWhiteSpace(envConnectionString))
        {
            return envConnectionString;
        }

        var parts = new Dictionary<string, string?>(StringComparer.Ordinal);
        foreach (var (builderKey, envVar) in PartMap)
        {
            parts[builderKey] = Environment.GetEnvironmentVariable(envVar);
        }

        var anyDatabaseEnvVarSet = parts.Values.Any(v => !string.IsNullOrWhiteSpace(v));
        if (anyDatabaseEnvVarSet)
        {
            var missing = PartMap
                .Where(x => string.IsNullOrWhiteSpace(parts[x.BuilderKey]))
                .Select(x => x.EnvVar)
                .ToArray();

            if (missing.Length > 0)
            {
                throw new InvalidOperationException(
                    $"If using DATABASE_* vars, all five must be set. Missing: {string.Join(", ", missing)}"
                );
            }

            if (!int.TryParse(parts["Port"], out var parsedPort))
            {
                throw new InvalidOperationException("DATABASE_PORT must be an integer.");
            }

            return new NpgsqlConnectionStringBuilder
            {
                Host = parts["Host"]!,
                Database = parts["Database"]!,
                Username = parts["Username"]!,
                Password = parts["Password"]!,
                Port = parsedPort,
            }.ConnectionString;
        }

        var nestedConnectionString = configuration["Data:DefaultConnection:ConnectionString"];
        if (!string.IsNullOrWhiteSpace(nestedConnectionString))
        {
            return nestedConnectionString;
        }

        throw new InvalidOperationException(
            "No ConnectionString source found. Checked env ConnectionString, DATABASE_* vars, and config Data:DefaultConnection:ConnectionString."
        );
    }
}
