//using System.Data;
//using Microsoft.Extensions.Options;
//using Npgsql;
//using Tasks.Persistence.Options;

//namespace Tasks.Persistence.Data
//{
//    internal sealed class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
//    {
//        private readonly ConnectionStringOptions _connectionString;
//        private NpgsqlConnection? _connection;

//        public SqlConnectionFactory(IOptions<ConnectionStringOptions> connectionString) => _connectionString = connectionString.Value;

//        public IDbConnection GetOpenConnection()
//        {
//            if ((_connection ??= new NpgsqlConnection(_connectionString)).State != ConnectionState.Open)
//            {
//                _connection.Open();
//            }

//            return _connection;
//        }

//        public void Dispose() => _connection?.Dispose();
//    }

//    internal interface ISqlConnectionFactory
//    {
//        IDbConnection GetOpenConnection();
//    }
//}
