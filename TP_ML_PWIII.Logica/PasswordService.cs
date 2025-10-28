using System;
using Microsoft.AspNetCore.Identity;

namespace TP_ML_PWIII.Logica
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password must not be empty.", nameof(password));

            return _hasher.HashPassword(null, password);
        }

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword) || string.IsNullOrEmpty(providedPassword))
                return false;

            var result = _hasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success
                   || result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
