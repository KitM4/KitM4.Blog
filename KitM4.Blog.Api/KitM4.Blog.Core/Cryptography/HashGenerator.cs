using System.Text;
using System.Security.Cryptography;

namespace KitM4.Blog.Core.Cryptography;

public class HashGenerator
{
    private const int SaltSize = 64;

    public static string GenerateSalt()
    {
        byte[] buffer = new byte[SaltSize];

        using RandomNumberGenerator generator = RandomNumberGenerator.Create();
        generator.GetBytes(buffer);

        return Convert.ToBase64String(buffer);
    }

    public static string GenerateHash(string password, string salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltBytes = Convert.FromBase64String(salt);
        byte[] passwordWithSalt = new byte[passwordBytes.Length + saltBytes.Length];

        Buffer.BlockCopy(passwordBytes, 0, passwordWithSalt, 0, passwordBytes.Length);
        Buffer.BlockCopy(saltBytes, 0, passwordWithSalt, passwordBytes.Length, saltBytes.Length);

        byte[] hashBytes = SHA256.HashData(passwordWithSalt);

        return Convert.ToBase64String(hashBytes);
    }
}