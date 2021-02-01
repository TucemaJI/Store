using System;
using System.Security.Cryptography;

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
            }
            return password;
        }
    }
}
