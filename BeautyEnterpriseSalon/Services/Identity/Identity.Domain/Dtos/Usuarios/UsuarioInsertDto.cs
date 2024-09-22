namespace Identity.Domain.Dtos.Usuarios;

public class UsuarioInsertDto
{
    public string? Email { get; init; }
    public string? Password { get; init; }
    public string? ConfirmPassword { get; init; }
}
