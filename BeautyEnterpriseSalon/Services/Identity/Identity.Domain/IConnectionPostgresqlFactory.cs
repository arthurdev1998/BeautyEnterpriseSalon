using System.Data;
using System.Linq.Expressions;
using BuildinBlocks.Core.Data;
using Identity.Domain.Entities;

namespace Identity.Domain;

public interface IConnectionPostgresqlFactory
{
    IDbConnection Connection();
    Task<Usuario> InsertUsuario(Usuario usuario);
    Task<IEnumerable<Usuario>> GetAllUsuarios();
    Task<Usuario> UpdateUsuario(Usuario usuario);
    Task<Usuario>? GetUsuarioByEmail(string email);
    void DeleteUsuario(int id);
    Task<PagedResults<Usuario>> PageSearcheUsuario(Expression<Func<Usuario>> expression);
}