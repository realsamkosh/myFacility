using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Payment.Core.Remitta.RemittaUtility
{
    public class RemittaPaymentCodes
    {
        public static string RemitaPaymentCodeFeedback(string p)
        {
            string desc = "";
            switch (p)
            {
                case "00":
                    desc = "Transaction Completed Successfully";
                    break;
                case "01":
                    desc = "Transaction Approved";
                    break;
                case "02":
                    desc = "Transaction Failed";
                    break;
                case "012":
                    desc = "User Aborted Transaction";
                    break;
                case "020":
                    desc = "Invalid User Authentication";
                    break;
                case "021":
                    desc = "Transaction Pending";
                    break;
                case "022":
                    desc = "Invalid Request";
                    break;
                case "023":
                    desc = "Service Type or Merchant Does not Existn";
                    break;
                case "025":
                    desc = "Payment Reference Generatedn";
                    break;
                case "029":
                    desc = "Invalid Bank Code";
                    break;
                case "30":
                    desc = "Insufficient Balance";
                    break;
                case "31":
                    desc = "No Funding Account";
                    break;
                case "32":
                    desc = " Invalid Date Format";
                    break;
                case "40":
                    desc = "Initial Request OK";
                    break;
                case "999":
                    desc = "Unknown Error";
                    break;
                default:
                    break;
            }


            return desc;
        }
    }
}
