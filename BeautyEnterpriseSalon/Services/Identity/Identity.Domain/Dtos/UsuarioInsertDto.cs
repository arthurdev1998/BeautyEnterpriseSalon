namespace Identity.Domain.Dtos;

public class UsuarioInsertDto
{
    public required string? Email { get; set; }
    public required string? Password { get; set; }
    public required string? ConfirmPassword { get; set; }
}