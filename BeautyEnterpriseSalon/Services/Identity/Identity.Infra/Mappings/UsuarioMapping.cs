using System.Security.Cryptography.X509Certificates;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infra.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        {
            builder.HasKey(x => x.Id).HasName("usuarios_pkey");
            builder.ToTable("usuarios", "public");

            builder.Property(x => x.Id)
                    .HasColumnName("pk_id");
            builder.Property(x => x.Email)
                    .HasColumnName("email")
                    .HasMaxLength(250);
            builder.Property(x => x.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(250);
            builder.Property(x => x.PasswordHash)
                    .HasColumnName("password_hash");
            builder.Property(x => x.PasswordSalt)
                    .HasColumnName("passord_salt");
        }
    }
}
