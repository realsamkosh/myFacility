using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace myFacility.Utilities
{
    public class Common
    {
        public static String GenerateFileName(String PatientNo)
        {
            String FileName = String.Format("{0}_{1}_{2}", PatientNo.Replace("/", "#"), DateTime.Now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid().ToString());
            return FileName;
        }


        public static decimal AmountToKobo(decimal amount)
        {
            return (decimal)(amount * 100);
        }


        public static decimal KoboToAmount(decimal amount)
        {
            return (decimal)(amount / 100);
        }

        public static string GenerateGTBReferenceNumber()
        {
            return"GTB" + DateTime.Now.Ticks;
        }


        public static string GTBSHA512(string hash_string)
        {
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            string hashed = BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
            return hashed;
        }


        public static string GetSHA512(string text)
        {
            ASCIIEncoding UE = new ASCIIEncoding();
            byte[] hashValue;
            byte[] data = UE.GetBytes(text);
            SHA512 hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(data);
            hex = ByteToString(hashValue);
            return hex;
        }

        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }


        public static string Tokenize(string amount)
        {
            return (amount.Split('.')[0].Replace(",", ""));
        }

        public static string xTokenize(string amount)
        {
            return (amount.Replace(",", ""));
        }

        public static string Randomize()
        {
            string number = "";
            for (int i = 0; i < 16; i++)
            {
                number = number + Convert.ToString(Rnd.Next(0, 9));
            }
            return number;
        }

        public static string Randomize(bool isGuid)
        {
            string guid = Guid.NewGuid().ToString();
            guid = guid.Replace("-", "").Substring(0, 10);
            return guid;
        }

        private static Random Rnd = new Random();
        public static string RandomizeNumber()
        {
            string number = "";
            for (int i = 0; i < 10; i++)
            {
                number = number + Convert.ToString(Rnd.Next(0, 9));
            }
            return number;
        }

        public static string RandomizePassword()
        {
            string number = "";
            for (int i = 0; i < 5; i++)
            {
                number = number + Convert.ToString(Rnd.Next(0, 9));
            }
            return number;
        }

        public static string SpellNumber(string MyNumber)
        {
            string Naria = "";
            string Kobo = "";
            string Temp = "";
            int DecimalPlace = 0;
            int Count = 0;
            string[] Place = new string[10];
            Place[2] = " Thousand ";
            Place[3] = " Million ";
            Place[4] = " Billion ";
            Place[5] = " Trillion ";
            // String representation of amount.
            //[Obsolete]
            //MyNumber = Strings.Trim(Conversion.Str(MyNumber));
            MyNumber = MyNumber.ToString().Trim();
            // Position of decimal place 0 if none.
            DecimalPlace = Strings.InStr(MyNumber, ".");
            // Convert Kobo and set MyNumber to Naria amount.
            if (DecimalPlace > 0)
            {
                //New Code
                var decimaPlaces = DecimalPlace + 1;
                var stringMid = MyNumber.Substring(decimaPlaces) + "00";
                var stringLeft = stringMid.Substring(0, 2);
                Kobo = GetTens(stringLeft);

                // Old Code
                // Kobo = GetTens(Strings.Left(Strings.Mid(MyNumber, DecimalPlace + 1) + "00", 2));
                var decimalPlaces2 = DecimalPlace - 1;
                var getLeft2 = MyNumber.Substring(decimalPlaces2);
                MyNumber = getLeft2.Trim();
            }
            Count = 1;
            while (!string.IsNullOrEmpty(MyNumber))
            {
                Temp = GetHundreds(Strings.Right(MyNumber, 3));
                if (!string.IsNullOrEmpty(Temp))
                    Naria = Temp + Place[Count] + Naria;
                if (Strings.Len(MyNumber) > 3)
                {
                    MyNumber = Strings.Left(MyNumber, Strings.Len(MyNumber) - 3);
                }
                else
                {
                    MyNumber = "";
                }
                Count = Count + 1;
            }

            if (Naria.Trim().EndsWith(" AND"))
            {
                Naria = Naria.Substring(0, Naria.Length - 1 - 4);
            }

            switch (Naria)
            {
                case "":
                    Naria = "";
                    break;
                case "One":
                    Naria = "One Naira";
                    break;
                default:
                    Naria = Naria + " Naira";
                    break;
            }
            switch (Kobo)
            {
                case "":
                    Kobo = "";
                    break;
                case "One":
                    Kobo = " and One Kobo";
                    break;
                default:
                    Kobo = " and " + Kobo + " Kobo";
                    break;
            }
            return Naria + Kobo + " only";
        }

        // Converts a number from 100-999 into text 
        public static string GetHundreds(string MyNumber)
        {
            string Result = "";
            if (Convert.ToInt32(MyNumber) == 0)
            {
                return string.Empty;
            }
            MyNumber = Strings.Right("000" + MyNumber, 3);
            // Convert the hundreds place.
            if (Strings.Mid(MyNumber, 1, 1) != "0")
            {
                Result = GetDigit(Strings.Mid(MyNumber, 1, 1)) + " Hundred ";
            }
            // Convert the tens and ones place.
            if (Strings.Mid(MyNumber, 2, 1) != "0")
            {
                if (string.IsNullOrEmpty(Result))
                {
                    Result = Result + GetTens(Strings.Mid(MyNumber, 2));
                }
                else
                {
                    Result = Result + "AND " + GetTens(Strings.Mid(MyNumber, 2));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Result))
                {
                    Result = Result + GetDigit(Strings.Mid(MyNumber, 3));
                }
                else
                {
                    Result = Result + "AND " + GetDigit(Strings.Mid(MyNumber, 3));
                }

            }
            return Result;
        }

        // Converts a number from 10 to 99 into text. 
        public static string GetTens(string TensText)
        {
            string Result = null;
            Result = "";
            // Null out the temporary function value.
            // If value between 10-19...
            if (Convert.ToInt32(Strings.Left(TensText, 1)) == 1)
            {
                switch (Convert.ToInt32(TensText))
                {
                    case 10:
                        Result = "Ten";
                        break;
                    case 11:
                        Result = "Eleven";
                        break;
                    case 12:
                        Result = "Twelve";
                        break;
                    case 13:
                        Result = "Thirteen";
                        break;
                    case 14:
                        Result = "Fourteen";
                        break;
                    case 15:
                        Result = "Fifteen";
                        break;
                    case 16:
                        Result = "Sixteen";
                        break;
                    case 17:
                        Result = "Seventeen";
                        break;
                    case 18:
                        Result = "Eighteen";
                        break;
                    case 19:
                        Result = "Nineteen";
                        break;
                    default:
                        break;
                }
                // If value between 20-99...
            }
            else
            {
                switch (Convert.ToInt32(Strings.Left(TensText, 1)))
                {
                    case 2:
                        Result = "Twenty ";
                        break;
                    case 3:
                        Result = "Thirty ";
                        break;
                    case 4:
                        Result = "Forty ";
                        break;
                    case 5:
                        Result = "Fifty ";
                        break;
                    case 6:
                        Result = "Sixty ";
                        break;
                    case 7:
                        Result = "Seventy ";
                        break;
                    case 8:
                        Result = "Eighty ";
                        break;
                    case 9:
                        Result = "Ninety ";
                        break;
                    default:
                        break;
                }
                Result = Result + GetDigit(Strings.Right(TensText, 1));
                // Retrieve ones place.
            }
            return Result;
        }

        // Converts a number from 1 to 9 into text. 
        public static string GetDigit(string Digit)
        {
            string functionReturnValue = null;
            switch (Convert.ToInt32(Digit))
            {
                case 1:
                    functionReturnValue = "One";
                    break;
                case 2:
                    functionReturnValue = "Two";
                    break;
                case 3:
                    functionReturnValue = "Three";
                    break;
                case 4:
                    functionReturnValue = "Four";
                    break;
                case 5:
                    functionReturnValue = "Five";
                    break;
                case 6:
                    functionReturnValue = "Six";
                    break;
                case 7:
                    functionReturnValue = "Seven";
                    break;
                case 8:
                    functionReturnValue = "Eight";
                    break;
                case 9:
                    functionReturnValue = "Nine";
                    break;
                default:
                    functionReturnValue = "";
                    break;
            }
            return functionReturnValue;
        }

        public static string ConvertObjectToJson(object myObject)
        {
          return  JsonConvert.SerializeObject(myObject, Newtonsoft.Json.Formatting.Indented);
        }

    }
}
