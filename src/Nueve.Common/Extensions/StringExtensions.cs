using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Nueve.Common.Extensions
{
    public static class StringExtensions
    {
        public static string GetHash(this string input)
        {
            HashAlgorithm hashAlgorithm = SHA256.Create();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static string ToKeyFormat(this string s)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(s));
        }
    }
}
