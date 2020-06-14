using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.SMSUtility.SMSProviderNG.Model
{
    public class ErrorCodes
    {
        public static string SMSProviderNGFeedbacks(string p)
        {
            string desc = "";
            switch (p)
            {
                case "000":
                    desc = "Request successful";
                    break;
                case "100":
                    desc = "Incomplete request parameters";
                    break;
                case "101":
                    desc = "Request denied";
                    break;
                case "110":
                    desc = "Login status failed";
                    break;
                case "111":
                    desc = "Login status denied";
                    break;
                case "120":
                    desc = "Message limit reached";
                    break;
                case "121":
                    desc = "Mobile limit reached";
                    break;
                case "122":
                    desc = "Sender limit reached";
                    break;
                case "130":
                    desc = "Sender prohibited";
                    break;
                case "131":
                    desc = "Message prohibited";
                    break;
                case "140":
                    desc = "Invalid price setup";
                    break;
                case "141":
                    desc = "Invalid route setup";
                    break;
                case "142":
                    desc = "Invalid schedule date";
                    break;
                case "150":
                    desc = "Insufficient funds";
                    break;
                case "151":
                    desc = "Gateway denied access";
                    break;
                case "152":
                    desc = "Service denied access";
                    break;
                case "160":
                    desc = "File upload error";
                    break;
                case "161":
                    desc = "File upload limit";
                    break;
                case "162":
                    desc = "File restricted";
                    break;
                case "190":
                    desc = "Maintenance in progress";
                    break;
                case "191":
                    desc = " Internal error";
                    break;     
                default:
                    break;
            }
            return desc;
        }
    }
}
