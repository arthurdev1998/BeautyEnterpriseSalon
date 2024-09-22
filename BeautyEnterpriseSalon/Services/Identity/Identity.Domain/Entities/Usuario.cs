using BuildinBlocks.Core.DomainObjects;
using Identity.Domain.Extensions;

namespace Identity.Domain.Entities;

public class Usuario : Entity<Guid>, IAgreegateRoot
{
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public DateTime CreateAt { get; set; }

    public override bool IsValid()
    {
        return false;
    }

    public void AddHashPassword(string password)
    {
        PasswordHash = SecurityExtensions.EncoderPassword(password);
    }

    public void AddSaltPassoword(byte[] hashPassword)
    {
        PasswordSalt = SecurityExtensions.CreatePasswordSalt(hashPassword);
    }
}