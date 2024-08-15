using Identity.Application.Commands;
using Identity.Domain.Entities;

namespace Identity.Application.Mappers;

public static class UsuarioMapperExtension
{
    public static Usuario MapToNewUsuario(this InsertUsuarioCommand dto)
    {
        return new Usuario
        {
            Id = Guid.NewGuid(),
            Email = dto.Email!,
            CreateAt = dto.CreateAt
        };
    }
}
