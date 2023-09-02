using System.Security.Cryptography;
using System.Text;

namespace Auth.API.Infrastructure.Securities
{
    public class Cryptography
    {
        public static string GenerateMD5(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                var builder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                    builder.Append(data[i].ToString("x2"));

                return builder.ToString();
            }
        }
    }
}