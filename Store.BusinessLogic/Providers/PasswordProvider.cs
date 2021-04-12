using System;
using System.Security.Cryptography;
using static Store.Shared.Constants.Constants;

namespace Store.BusinessLogic.Providers
{
    public class PasswordProvider
    {

        public string GeneratePassword(byte length)
        {
            string password = string.Empty;
            using (RNGCryptoServiceProvider cryptRNG = new RNGCryptoServiceProvider())
            {
                byte[] tokenBuffer = new byte[length];
                cryptRNG.GetBytes(tokenBuffer);
                password = Convert.ToBase64String(tokenBuffer);
                Random random = new Random();
                password += PasswordConsts.LOWERCASE.ToCharArray()[random.Next(PasswordConsts.LOWERCASE.Length)];
                password += PasswordConsts.UPPERCASE.ToCharArray()[random.Next(PasswordConsts.UPPERCASE.Length)];
                password += PasswordConsts.NUMBERS.ToCharArray()[random.Next(PasswordConsts.NUMBERS.Length)];
                password += PasswordConsts.SYMBOLS.ToCharArray()[random.Next(PasswordConsts.SYMBOLS.Length)];
            }
            return password;
        }
    }
}
