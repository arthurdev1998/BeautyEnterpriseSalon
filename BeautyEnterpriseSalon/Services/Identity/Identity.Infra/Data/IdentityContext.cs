using Identity.Domain.Entities;
using Identity.Domain.Interfaces.ContextInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infra.Data;

public class IdentityContext : DbContext , IUnitoOfWork
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }

    public async Task<int> Commit()
    {
        return await base.SaveChangesAsync();
    }
}
