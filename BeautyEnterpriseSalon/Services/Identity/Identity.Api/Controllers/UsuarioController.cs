using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BuildinBlocks.Core.Data;
using Dapper;
using Identity.Api.Configurations.Options;
using Identity.Application.DbConnection;
using Identity.Application.Dtos;
using Identity.Application.Extension.DbExtensions;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Identity.Api.Controllers;

[Route("/api")]
public class UsuarioController : ControllerBase
{
    private readonly IOptions<ConnectionSettings> _options;
    private readonly IConnectionPostgresqlFactory _connectionFactory;

    public UsuarioController(IOptions<ConnectionSettings> options, IConnectionPostgresqlFactory connectionFactory)
    {
        _options = options;
        _connectionFactory = connectionFactory;
    }

    [HttpPost("{id}")]
    public async Task<Teste> GetToken(int id)
    {
        using (IDbConnection dbConnection = new NpgsqlConnection(_options.Value.DbConnection))
        {
            var parameter = new { id };
            string sql = "SELECT * FROM teste WHERE id = @Id";
            var result = dbConnection.QuerySingleOrDefault<Teste>(sql, parameter);

            return result;
        }

        // var tokenHandler = new JwtSecurityTokenHandler();
        // var key = Encoding.ASCII.GetBytes(_options.Value.Secret);

        // var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        // {
        //     Issuer = _options.Value.Emisor,
        //     Audience = _options.Value.ValidoEm,
        //     Expires = DateTime.UtcNow.AddHours(2),
        //     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        // });

        // var encodedToken = tokenHandler.WriteToken(token);
        // return Ok(encodedToken);
    }

    [HttpGet]
    public async Task<PagedResults<Teste>> GetPaged(TesteParametersDto testeParametersDto)
    { 
        string sql = @$"SELECT ID AS {nameof(Teste.Id)}, 
                        NOME AS {nameof(Teste.Nome)},
                        EMAIL AS {nameof(Teste.Email)} FROM TESTE WHERE 1=1";
        
        using(var connection = _connectionFactory.Connection())
        {
            connection.Open();

            var result = await connection.PagedQueryAsync<Teste>(sql, testeParametersDto.PageIndex, testeParametersDto.PageSize);

            return result;
        }
    }
}