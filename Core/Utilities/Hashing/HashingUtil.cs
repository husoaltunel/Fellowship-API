using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Hashing
{
    public static class HashingUtil
    {
        private static byte[] _passwordHash;
        private static byte[] _passwordSalt;
        public static void GeneratePasswordHashAndSalt(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                _passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                _passwordSalt = hmac.Key;
            }
        }
        public static byte[] GetPasswordHash()
        {
            return _passwordHash;
        }
        public static byte[] GetPasswordSalt()
        {
            return _passwordSalt;
        }
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                hmac.Key = passwordSalt;
                _passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return CheckPasswordHashEqual(passwordHash);
            }
        }
        private static bool CheckPasswordHashEqual(byte[] passwordHash)
        {
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != _passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
