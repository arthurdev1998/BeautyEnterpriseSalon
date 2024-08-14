using BuildinBlocks.Core.Messages;
using FluentValidation.Results;
using Identity.Application.Validations;

namespace Identity.Application.Commands;

public class InsertUsuarioCommand : Command<string, bool>
{
    public required string? Email { get; set; }
    public required string? Password { get; set; }
    public required string? ConfirmPassword { get; set; }
    public DateTime CreateAt { get; init; } = DateTime.UtcNow;

    public ValidationResult IsValid()
    {
        return new InsertUsuarioValidation().Validate(this);
    }
}