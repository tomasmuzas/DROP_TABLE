using System;
using System.Security.Cryptography;
using System.Text;
using BackendApartmentReservation.Infrastructure.Utilities;

namespace BackendApartmentReservation.Authentication
{
    public class SecurityManager
    {
        public static string GetPasswordHash(Password password)
        {
            var hash = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashedPasswordBytes = hash.ComputeHash(passwordBytes);
            return BitConverter.ToString(hashedPasswordBytes).Replace("-", string.Empty);
        }
    }
}
