﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Security
{
    public class PasswordService : IPasswordService
    {
        
        private const string Base64SecretKey = "rspdZ40UHl1ZIZeIJQ+AVT+YCrxsx33d0J/s/tC+Cyk=";
        private readonly byte[] _secretKey;

        public PasswordService()
        {
            _secretKey = Convert.FromBase64String(Base64SecretKey);
        }

        public string HashPassword(string password)
        {
            byte[] salt = GenerateSalt(password);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }





        public bool ValidatePassword(string password, string hashedPassword)
        {
            byte[] salt = GenerateSalt(password);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed == hashedPassword;
        }


        private byte[] GenerateSalt(string password)
        {
            using (var hmac = new HMACSHA256(_secretKey))
            {
                return hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
