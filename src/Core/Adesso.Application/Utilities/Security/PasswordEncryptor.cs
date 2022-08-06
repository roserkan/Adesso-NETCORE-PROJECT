using System.Security.Cryptography;
using System.Text;

namespace Adesso.Application.Utilities.Security;

public class PasswordEncryptor
{
    public static string Encrypt(string password)
    {
        using var md5 = MD5.Create();

        byte[] inputBytes = Encoding.ASCII.GetBytes(password);
        byte[] outputBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(outputBytes);
    }
}