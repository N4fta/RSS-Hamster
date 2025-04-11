using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net;

namespace Domain
{
    public static class PasswordHashing
    {
        public static string HashPassword(string password)
        {
            return BC.BCrypt.EnhancedHashPassword(password);
        }

        internal static bool VerifyPassword(string text, string hash)
        {
            return BC.BCrypt.EnhancedVerify(text, hash);
        }
    }
}
