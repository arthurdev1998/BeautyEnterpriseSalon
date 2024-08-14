using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Identity.Infra.Data;

public class DapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_configuration.GetConnectionString("SqlConnection"));
}
