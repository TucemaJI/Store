using System;
using System.Security.Cryptography;
using static Store.Shared.Constants.Constants;

namespace Store.BusinessLogic.Providers
{
    public class PasswordProvider
    {

        public string GeneratePassword(byte length)
        {
            var password = string.Empty;
            using (RNGCryptoServiceProvider cryptRNG = new RNGCryptoServiceProvider())
            {
                byte[] tokenBuffer = new byte[length];
                cryptRNG.GetBytes(tokenBuffer);
                password = Convert.ToBase64String(tokenBuffer);
                Random random = new Random();
                password += PasswordOptions.LOWERCASE.ToCharArray()[random.Next(PasswordOptions.LOWERCASE.Length)];
                password += PasswordOptions.UPPERCASE.ToCharArray()[random.Next(PasswordOptions.UPPERCASE.Length)];
                password += PasswordOptions.NUMBERS.ToCharArray()[random.Next(PasswordOptions.NUMBERS.Length)];
                password += PasswordOptions.SYMBOLS.ToCharArray()[random.Next(PasswordOptions.SYMBOLS.Length)];
            }
            return password;
        }
    }
}
