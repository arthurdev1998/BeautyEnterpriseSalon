using Microsoft.AspNetCore.Http.HttpResults;

namespace Identity.Application.Dtos;

public class UsuarioDto
{
    public string? Email { get; set; }
    public bool Created {get; set;}
}
