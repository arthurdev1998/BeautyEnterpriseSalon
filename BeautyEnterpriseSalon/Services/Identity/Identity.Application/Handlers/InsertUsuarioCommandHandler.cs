using System.Data;
using BuildinBlocks.Core.Messages;
using Dapper;
using Identity.Application.Commands;
using Identity.Application.DbConnection;
using Identity.Application.Mappers;
using MediatR;

namespace Identity.Application.Handlers;

public class InsertUsuarioCommandHandler : IRequestHandler<InsertUsuarioCommand, CommandResult<InsertUsuarioCommand>>
{
    private readonly IConnectionPostgresqlFactory _connectionPostgresql;

    public InsertUsuarioCommandHandler(IConnectionPostgresqlFactory connectionPostgresql)
    {
        _connectionPostgresql = connectionPostgresql;
    }

    public async Task<CommandResult<InsertUsuarioCommand>> Handle(InsertUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (!request.ConfirmPasswordIsValid())
        {
            return CommandResult<InsertUsuarioCommand>.Fail("Senhas nao coincidem");
        }

        if (!request.Validation().IsValid)
        {
            return CommandResult<InsertUsuarioCommand>.Fail(request.Validation().Errors.SelectMany(x => x.ErrorMessage).ToString()!);
        }

        var usuario = request.MapToNewUsuario();

        usuario.AddHashPassword(request.Password!);
        usuario.AddSaltPassoword();

        using (IDbConnection dbConnection = _connectionPostgresql.Connection())
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
                    return new CommandResult<InsertUsuarioCommand>(); // Sucesso
                }
                else
                {
                    return CommandResult<InsertUsuarioCommand>.Fail("Houve um erro ao salvar no banco de dados"); // Falha
                }
            }
            catch (Exception ex)
            {
                // Captura e trata exceções
                return CommandResult<InsertUsuarioCommand>.Fail($"Erro: {ex.Message}");
            }
        }
    }
}