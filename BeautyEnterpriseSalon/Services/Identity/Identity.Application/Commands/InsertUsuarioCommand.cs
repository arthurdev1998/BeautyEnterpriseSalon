using BuildinBlocks.Core.Messages;
using FluentValidation.Results;
using Identity.Application.Validations;
using Identity.Domain.Dtos;

namespace Identity.Application.Commands;

public class InsertUsuarioCommand : Command<string, CommandResult<InsertUsuarioCommand>>
{
    public  string? Email { get; set; }
    public  string? Password { get; set; }
    public  string? ConfirmPassword { get; set; }
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