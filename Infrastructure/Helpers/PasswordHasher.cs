using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Helpers
{
    public class PasswordHasher
    {

        public static (string, string) GenerateSecurePassword(string password)
        {
            try
            {
                using var hmac = new HMACSHA512();

                var securePassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                var securityKey = hmac.Key;

                return (Convert.ToBase64String(securePassword), Convert.ToBase64String(securityKey));

            }
            catch
            { return (null!, null!); }

        }

        public static bool ValidateSecurePassword(string password, string passwordToCheck, string securityKey)
        {
            try
            {
                var userPassword = Convert.FromBase64String(passwordToCheck);
                var security = Convert.FromBase64String(securityKey);

                using var hmac = new HMACSHA512(security);
                var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < hashedPassword.Length; i++)
                {
                    if (hashedPassword[i] != userPassword[i])
                        return false;
                }
                return true;
            }
            catch 
            { return false; }

        }
    }
}
