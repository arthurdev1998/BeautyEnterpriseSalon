using System.Security.Cryptography;
using System.Text;

namespace Identity.Domain.Extensions;

public static class SecurityExtensions
{
    public static byte[] EncoderPassword(string password)
    {
        using var hmac = new HMACSHA512();
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        return hmac.ComputeHash(passwordBytes);
    }

    public static byte[] CreatePasswordSalt()
    {
        using var hmac = new HMACSHA512();
        return hmac.Key;
    }
}