using BuildinBlocks.Core.Messages;
using FluentValidation.Results;
using Identity.Application.Dtos;
using Identity.Application.Validations;
using Identity.Domain.Dtos;
using Identity.Domain.Dtos.Usuarios;

namespace Identity.Application.Commands;

public class InsertUsuarioCommand : Command<string, CommandResult<UsuarioDto>>
{
    public  string? Email { get; init; }
    public  string? Password { get; init; }
    public  string? ConfirmPassword { get; init; }
    public DateTime CreateAt { get; init; } = DateTime.UtcNow;

    public ValidationResult Validation()
    {
        return new InsertUsuarioValidation().Validate(this);
    }

    public bool ConfirmPasswordIsValid()
    {
        return Password == ConfirmPassword;
    }

    public InsertUsuarioCommand(UsuarioInsertDto dto)
    {
        Email = dto.Email;
        Password = dto.Password;
        ConfirmPassword = dto.ConfirmPassword;
    }
}