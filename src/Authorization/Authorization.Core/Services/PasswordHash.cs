using System.Security.Cryptography;
using System.Text;

namespace Authorization.Core.Services
{
    public class PasswordHash
    {
        public static string ComputeSHA512HashFromString(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            byte[] hashBytes = SHA512.HashData(inputBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "");
        }
    }
}