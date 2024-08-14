using System.Data;

namespace Identity.Application.DbConnection;

public interface IConnectionPostgresqlFactory
{
    IDbConnection Connection();
}