using BuildinBlocks.Core.DomainObjects;

namespace Identity.Domain.Entities;

public class Usuario: Entity<Guid>, IAgreegateRoot
{
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }

    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
}