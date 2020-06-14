using System;
using System.Text;
using System.Security.Cryptography;

namespace myFacility.Utilities
{
    public class PasswordGenerator
    {
        public  static string CreateNumericRandomPassword(int length)
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

        public static string CreateLowCaseRandomPassword(int length)
        {
            const string valid = "abcdefghijkmnopqrstuvwxyz";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static string CreateUpperCaseRandomPassword(int length)
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

        public static string CreateSpecRandomPassword(int length)
        {
            const string valid = "!@$?_-#";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static string CreateRandom(int passwordLength, bool Uppercase, bool RequireDigit, bool Lowercase)
        {
            if (RequireDigit == true && Uppercase == true && Lowercase == true)
            {
                string passcode = DateTime.Now.Ticks.ToString();
                string UpperCaseTrue1 = CreateUpperCaseRandomPassword(1);
                string LowerCaseTrue1 = CreateLowCaseRandomPassword(1);
                string passc1 = CreateSpecRandomPassword(1);
                string pass = BitConverter.ToString(new SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(passcode))).Replace("-", LowerCaseTrue1);
                var password = pass.Substring(0, passwordLength) + passc1 + UpperCaseTrue1;
                StringBuilder res = new StringBuilder();
                res.Append(password);
                return res.ToString();
            }
            else if (RequireDigit == true && Uppercase == false && Lowercase == false)
            {
                string passcode = DateTime.Now.Ticks.ToString();
                string NumericTrue2 = CreateNumericRandomPassword(1);
                string passc2 = CreateSpecRandomPassword(1);
                string pass = BitConverter.ToString(new SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(passcode))).Replace("-", NumericTrue2);
                var password = pass.Substring(0, passwordLength) + passc2;
                StringBuilder res = new StringBuilder();
                res.Append(password);
                return res.ToString();
            }
            else if (RequireDigit == true && Uppercase == true && Lowercase == false)
            {
                string passcode = DateTime.Now.Ticks.ToString();
                string NumericTrue3 = CreateNumericRandomPassword(1);
                string UpperTrue3 = CreateUpperCaseRandomPassword(1);
                string passc3 = CreateSpecRandomPassword(1);
                string pass = BitConverter.ToString(new SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(passcode))).Replace("-", NumericTrue3);
                var password = pass.Substring(0, passwordLength) + passc3 + UpperTrue3;
                StringBuilder res = new StringBuilder();
                res.Append(password);
                return res.ToString();
            }
            else if (RequireDigit == false && Uppercase == true && Lowercase == false)
            {
                string UpperTrue4 = CreateUpperCaseRandomPassword(1);
                string pass = BitConverter.ToString(new SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(UpperTrue4)));
                var password = pass.Substring(0, passwordLength);
                StringBuilder res = new StringBuilder();
                res.Append(password);
                return res.ToString();
            }
            else if (RequireDigit == false && Uppercase == false && Lowercase == true)
            {
                string LowerTrue4 = CreateLowCaseRandomPassword(1);
                string pass = BitConverter.ToString(new SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(LowerTrue4)));
                var password = pass.Substring(0, passwordLength);
                StringBuilder res = new StringBuilder();
                res.Append(password);
                return res.ToString();
            }
            string passcode5 = DateTime.Now.Ticks.ToString();
            string UpperCaseTrue5 = CreateUpperCaseRandomPassword(1);
            string LowerCaseTrue5 = CreateLowCaseRandomPassword(1);
            string passc5 = CreateSpecRandomPassword(1);
            string pass5 = BitConverter.ToString(new SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(passcode5))).Replace("-", LowerCaseTrue5);
            var password5 = pass5.Substring(0, passwordLength) + passc5 + UpperCaseTrue5;
            StringBuilder res5 = new StringBuilder();
            res5.Append(password5);
            return res5.ToString();
        }
    }
}
