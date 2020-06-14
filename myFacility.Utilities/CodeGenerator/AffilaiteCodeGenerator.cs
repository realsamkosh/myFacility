using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace myFacility.Utilities.CodeGenerator
{
    public class AffilaiteCodeGenerator
    {
        public static string CreateNumericRandom(int length)
        {
            const string valid = "0123456789";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static string CreateUpperCaseRandom(int length)
        {
            const string valid = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static string CreateRandom(int passwordLength)
        {
            string passcode5 = DateTime.Now.Ticks.ToString();
            string uppercase = CreateUpperCaseRandom(1);
            string numeric = CreateNumericRandom(1);
            string pass5 = BitConverter.ToString(new SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(passcode5)))
                .Replace("-", uppercase);
            var password5 = pass5.Substring(0, passwordLength) + numeric + uppercase;
            StringBuilder res5 = new StringBuilder();
            res5.Append(password5);
            return res5.ToString();
        }
    }
}
