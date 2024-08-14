using System.Data;
using Npgsql;

namespace Identity.Application.DbConnection;

public class ConnectionPostgresqlFactory : IConnectionPostgresqlFactory
{
    private string _connectionString;

    public ConnectionPostgresqlFactory(string configuration)
    {
        _connectionString = configuration; 
    }

    public IDbConnection Connection() => new NpgsqlConnection(_connectionString);
}
