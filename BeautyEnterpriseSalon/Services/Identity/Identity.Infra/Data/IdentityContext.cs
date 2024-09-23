using Identity.Domain.Entities;
using Identity.Domain.Interfaces.ContextInterfaces;
using Identity.Domain.Messages;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infra.Data;

public class IdentityContext : DbContext, IUnitoOfWork
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<HttpRequestLog> HttpRequestLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityContext).Assembly);
    }

    public async Task<int> Commit()
    {
        return await base.SaveChangesAsync();
    }
}
