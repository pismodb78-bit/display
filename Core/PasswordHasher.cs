using System;
using System.Security.Cryptography;
using System.Text;

namespace SchoolSchedule.Core
{
    /// <summary>
    /// Пароль учителя в базе лежит хешем, а не текстом: базу видно в
    /// phpMyAdmin, и заглянувший туда ученик не должен получить пароль от
    /// редактора. Формат тот же, что в PISMO: pbkdf2$итерации$соль$хеш.
    /// </summary>
    public static class PasswordHasher
    {
        private const int Iterations = 100000;
        private const int SaltBytes = 16;
        private const int HashBytes = 32;
        private const string Prefix = "pbkdf2$";

        public static string Hash(string password)
        {
            if (password == null) password = "";

            var salt = RandomNumberGenerator.GetBytes(SaltBytes);
            var hash = Derive(password, salt, Iterations, HashBytes);

            return Prefix + Iterations + "$" + Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Проверка. Если в базе лежит не хеш, а обычный текст (так бывает,
        /// когда пароль вписали руками через phpMyAdmin), сравниваем как есть —
        /// иначе человек окажется заперт снаружи собственной программы.
        /// </summary>
        public static bool Verify(string password, string stored)
        {
            if (string.IsNullOrEmpty(stored)) return false;
            if (password == null) password = "";

            if (!stored.StartsWith(Prefix, StringComparison.Ordinal))
                return FixedTimeEquals(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(stored));

            var parts = stored.Split('$');
            if (parts.Length != 4) return false;

            int iterations;
            if (!int.TryParse(parts[1], out iterations) || iterations <= 0) return false;

            byte[] salt, expected;
            try
            {
                salt = Convert.FromBase64String(parts[2]);
                expected = Convert.FromBase64String(parts[3]);
            }
            catch { return false; }

            var actual = Derive(password, salt, iterations, expected.Length);
            return FixedTimeEquals(actual, expected);
        }

        /// <summary>Хеш ли это, или в базу вписали открытый текст.</summary>
        public static bool IsHashed(string stored)
        {
            return stored != null && stored.StartsWith(Prefix, StringComparison.Ordinal);
        }

        private static byte[] Derive(string password, byte[] salt, int iterations, int length)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
                return pbkdf2.GetBytes(length);
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}
