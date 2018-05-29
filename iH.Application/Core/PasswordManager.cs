namespace iH.Application.Core
{
    using System;
    using System.Security.Cryptography;

    internal static class PasswordManager
    {
        private const int Iterations = 10000;
        private const int SaltSize = 16;
        private const int HashSize = 20;

        internal static string HashPassword(string password, out string saltText)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            saltText = Convert.ToBase64String(salt);
            return Convert.ToBase64String(hash);
        }

        internal static bool MatchPassword(string password, string savedPassword, string savedSalt)
        {
            byte[] hash = Convert.FromBase64String(savedPassword);
            byte[] salt = Convert.FromBase64String(savedSalt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] newHash = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
                if (hash[i] != newHash[i])
                    return false;

            return true;
        }
    }
}
