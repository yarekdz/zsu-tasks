using System.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;
using Tasks.Persistence.Data;

namespace Tasks.Persistence.Health
{
    internal sealed class DatabaseHealthCheck : IHealthCheck
    {
        private readonly NpgSqlDbConnectionFactory _npgSqlDbConnectionFactory;

        public DatabaseHealthCheck(NpgSqlDbConnectionFactory npgSqlDbConnectionFactory)
        {
            this._npgSqlDbConnectionFactory = npgSqlDbConnectionFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken ct = new CancellationToken())
        {
            try
            {
                await using var connection = await _npgSqlDbConnectionFactory.CreateConnectionAsync(ct);
                await using var command = new NpgsqlCommand("SELECT '8'", connection);
                await using var reader = await command.ExecuteReaderAsync(ct);

                return HealthCheckResult.Healthy();
            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy(exception: e);
            }
        }
    }
}
