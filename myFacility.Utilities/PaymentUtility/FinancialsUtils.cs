using System;
using System.Collections;
using System.Collections.Specialized;

namespace myFacility.Utilities
{
    public class FinancialsUtils
    {
        public static string FormatNaira(decimal n, int curr)
        {
            string ret = "";
            //string ret = System.Configuration.ConfigurationManager.AppSettings["myFacilityBaseCurrency"] + " "; //"₦ "; //  + n.ToString("#,##.##");
            if (curr == 7)
            {
                ret = "$ ";
            }
            else if (curr == 8)
            {
                ret = "£ ";
            }
            return ret + n.ToString("#,##.#0");
        }

      
        
        public static string InterSwitchFeedback(string p)
        {
            string desc = "";
            switch (p)
            {
                case "01":
                    {
                        desc = "Refer to card issuer";
                    }
                    break;
                case "02":
                    {
                        desc = "Refer to card issuer, special condition";
                    }
                    break;
                case "03":
                    {
                        desc = "Invalid merchant";
                    }
                    break;
                case "04":
                    {
                        desc = "Pick-up card";
                    }
                    break;
                case "05":
                    {
                        desc = "Do not honor";
                    }
                    break;
                case "06":
                    {
                        desc = "Error";
                    }
                    break;
                case "07":
                    {
                        desc = "Pick-up card, special condition";
                    }
                    break;
                case "08":
                    {
                        desc = "Honor with identification";
                    }
                    break;
                case "09":
                    {
                        desc = "Request in progress";
                    }
                    break;
                case "10":
                    {
                        desc = "Approved, partial";
                    }
                    break;
                case "11":
                    {
                        desc = "Approved, VIP";
                    }
                    break;
                case "12":
                    {
                        desc = "Invalid transaction";
                    }
                    break;
                case "13":
                    {
                        desc = "Invalid amount";
                    }
                    break;
                case "14":
                    {
                        desc = "Invalid card number";
                    }
                    break;
                case "15":
                    {
                        desc = "No such issuer";
                    }
                    break;
                case "16":
                    {
                        desc = "Approved, update track 3";
                    }
                    break;
                case "17":
                    {
                        desc = "Customer cancellation";
                    }
                    break;
                case "18":
                    {
                        desc = "Customer dispute";
                    }
                    break;
                case "19":
                    {
                        desc = "Re-enter transaction";
                    }
                    break;
                case "20":
                    {
                        desc = "Invalid response";
                    }
                    break;
                case "21":
                    {
                        desc = "No action taken";
                    }
                    break;
                case "22":
                    {
                        desc = "Suspected malfunction";
                    }
                    break;
                default:
                    {
                        desc = "Payment status could not be verified";
                    }
                    break;
            }
            return desc;
        }

        /// <summary>
        /// 
        /// </summary>
        public const string PaymentFor = "Amount: {0} Paid for {1} on {2}.";
    }
}
