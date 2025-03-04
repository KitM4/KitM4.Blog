using System.Text;
using System.Security.Cryptography;

namespace KitM4.Blog.Core.Cryptography;

/// <summary>
/// Provides methods to generate cryptographic salt and hash for password security
/// </summary>
public class HashGenerator
{
    /// <summary>
    /// Size of the salt in bytes
    /// </summary>
    private const int SaltSize = 64;

    /// <summary>
    /// Generates a cryptographic random salt
    /// </summary>
    /// <returns>Base64-encoded salt string</returns>
    public static string GenerateSalt()
    {
        byte[] buffer = new byte[SaltSize];

        using RandomNumberGenerator generator = RandomNumberGenerator.Create();
        generator.GetBytes(buffer);

        return Convert.ToBase64String(buffer);
    }

    /// <summary>
    /// Generates a SHA256 hash based on the provided password and salt
    /// </summary>
    /// <param name="password">The plain text password</param>
    /// <param name="salt">Base64-encoded salt string</param>
    /// <returns>Base64-encoded hash string</returns>
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