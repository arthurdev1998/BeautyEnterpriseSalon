using System.Data;
using System.Linq.Expressions;
using BuildinBlocks.Core.Data;
using Dapper;
using Identity.Domain;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Identity.Infra.Data.Repositories;

public class ConnectionPostgresqlFactory : IConnectionPostgresqlFactory
{
    private string _connectionString;
    private readonly IdentityContext _context;

    public ConnectionPostgresqlFactory(string configuration, IdentityContext context)
    {
        _connectionString = configuration;
        _context = context;
    }

    public ConnectionPostgresqlFactory(string configuration)
    {
        _connectionString = configuration;
    }
    public IDbConnection Connection() => new NpgsqlConnection(_connectionString);

    public void DeleteUsuario(int id)
    {
        var sql = @"DELETE * FROM public.usuarios WHERE id = @Id";

        var command = new { Id = id };

        using (IDbConnection dbConnection = Connection())
        {
            dbConnection.Open();

            var result = dbConnection.ExecuteAsync(sql, command);
        }

    }

    public async Task<IEnumerable<Usuario>> GetAllUsuarios()
    {
        string sql = "SELECT id, email, nome, password_hash, password_salt FROM public.usuarios";

        using (IDbConnection dbConnection = Connection())
        {
            dbConnection.Open();

            var result = await dbConnection.QueryAsync<Usuario>(sql);

            return result;
        }
    }

    public async Task<Usuario> InsertUsuario(Usuario usuario)
    {
        using (IDbConnection dbConnection = Connection())
        {
            dbConnection.Open();

            // Correção da string SQL
            string sql = @$"INSERT INTO public.usuarios
            {nameof(usuario.Email)}, 
            {nameof(usuario.Nome)}, 
            password_hash, 
            password_salt)

            VALUES 
                (@Email, @Nome, @PasswordHash, @PasswordSalt)";

            // Definindo os parâmetros corretamente
            var command = new
            {
                Nome = "naruto",
                Email = usuario.Email,
                PasswordHash = usuario.PasswordHash,
                PasswordSalt = usuario.PasswordSalt
            };

            try
            {
                // Executa a inserção no banco de dados
                var result = await dbConnection.ExecuteAsync(sql, command);

                if (result > 0)
                {
                    return usuario; // Sucesso
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir usuário: {ex.Message}");
            }
        }

        return null;
    }

    public Task<PagedResults<Usuario>> PageSearcheUsuario(Expression<Func<Usuario>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<Usuario> UpdateUsuario(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);

        if (await _context.Commit() > 0)
        {
            return usuario;
        }

        throw new Exception();
    }

    public async Task<Usuario>? GetUsuarioByEmail(string email)
    {
        var usuario = await _context.Usuarios
        .Where(x => x.Email == email)
        .Select(x => new Usuario { Email = x.Email, PasswordHash = x.PasswordHash })
        .FirstOrDefaultAsync();

        return usuario;

    }
}