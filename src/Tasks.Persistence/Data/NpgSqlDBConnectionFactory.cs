using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;
using Tasks.Persistence.Options;

namespace Tasks.Persistence.Data
{
    internal sealed class NpgSqlDbConnectionFactory : IDisposable
    {
        private readonly ConnectionStringOptions _connectionString;
        private NpgsqlConnection? _connection;

        public NpgSqlDbConnectionFactory(IOptions<ConnectionStringOptions> connectionString) =>
            _connectionString = connectionString.Value;

        public async Task<NpgsqlConnection> CreateConnectionAsync(CancellationToken ct)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_connectionString.Database);
            await using var dataSource = dataSourceBuilder.Build();

            if ((_connection ??= await dataSource.OpenConnectionAsync(ct)).State !=
                ConnectionState.Open)
            {
                await _connection.OpenAsync(ct);
            }

            return _connection;
        }

        public void Dispose() => _connection?.Dispose();
    }
}
