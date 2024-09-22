using System.Data;
using BuildinBlocks.Core.Messages;
using Dapper;
using FluentValidation.Results;
using Identity.Application.Commands;
using Identity.Application.Dtos;
using Identity.Application.Mappers;
using Identity.Domain;
using MediatR;

namespace Identity.Application.Handlers;

public class InsertUsuarioCommandHandler : IRequestHandler<InsertUsuarioCommand, CommandResult<UsuarioDto>>
{
    private readonly IConnectionPostgresqlFactory _connectionPostgresql;

    public InsertUsuarioCommandHandler(IConnectionPostgresqlFactory connectionPostgresql)
    {
        _connectionPostgresql = connectionPostgresql;
    }

    public async Task<CommandResult<UsuarioDto>> Handle(InsertUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (!request.ConfirmPasswordIsValid())
        {
            return CommandResult<UsuarioDto>.Fail("Senhas nao coincidem");
        }

        if (!request.Validation().IsValid)
        {
            return CommandResult<UsuarioDto>.Fail(string.Join("", request.Validation().Errors.SelectMany(x => x.ErrorMessage)));
        }

        var usuario = request.MapToNewUsuario();

        usuario.AddHashPassword(request.Password!);
        usuario.AddSaltPassoword(usuario.PasswordHash!);

        var response = await _connectionPostgresql.InsertUsuario(usuario);

        if (response is null)
        {
            CommandResult<UsuarioDto>.Fail("Erro na query de inserção");
        }

        return new CommandResult<UsuarioDto>(usuario.MapToUsuarioDto());
    }
}