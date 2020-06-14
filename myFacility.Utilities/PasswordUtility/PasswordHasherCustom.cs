using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace myFacility.Utilities.PasswordUtility
{
    public class PasswordHasherCustom
    {
        private const int PBKDF2IterCount = 10000;
        private const int PBKDF2SubkeyLength = 256 / 8;
        private const int SaltSize = 128 / 8;

        public string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            // Produce a version 0 (see comment above) text hash.
            byte[] salt;
            byte[] subkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, PBKDF2IterCount))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }

            var outputBytes = new byte[1 + SaltSize + PBKDF2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, PBKDF2SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

    }
}
