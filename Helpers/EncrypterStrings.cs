using System;
using System.Text;

namespace employee_raffles.Helpers
{
    public static class EncrypterStrings
    {
        public static string Encrypt(this string text)
        {
            byte[] encryted = Encoding.Unicode.GetBytes(text);
            string result = Convert.ToBase64String(encryted);
            return result;
        }
        public static string Decrypt(this string text)
        {
            byte[] decryted = Convert.FromBase64String(text);
            string result = Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
