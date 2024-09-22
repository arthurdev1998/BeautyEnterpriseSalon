using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces.ContextInterfaces;

public interface IUsuarioRepository
{
    Task<Usuario> InsertUsuario(Usuario usuario);
    Task<Usuario> UpdateUsuario(Usuario usuario);
    void RemoveUsuario(Usuario usuario);
}
