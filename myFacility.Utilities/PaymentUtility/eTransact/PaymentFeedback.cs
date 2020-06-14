using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace myFacility.Utilities.PaymentUtility.eTransact
{
    public class PaymentFeedback
    {
        public static Hashtable geteTransactFeedBack(string resp)
        {
            Hashtable res = new Hashtable();
            string respCode = "";
            if (resp == "-1")
            {
                respCode = "-1";
                res.Add("status", respCode == "00" ? "true" : "false");
                res.Add("code", respCode);
                res.Add("Desc", eTransactFeedback(Convert.ToInt16(respCode)));
                res.Add("webPayRef", "");
            }
            else
            {
                NameValueCollection webPayFeedback = new NameValueCollection();
                //Split the response from WEBPAY
                string[] tmp = resp.Split('&');

                string date = tmp[0];
                string CardL4Dig = tmp[1];
                string pDesc = tmp[2];
                respCode = tmp[3];
                //Get the Response Description from the config file by passing in respCode;
                res.Add("status", respCode == "0" ? "true" : "false");
                res.Add("code", respCode);
                res.Add("Desc", pDesc);
                res.Add("webPayRef", "");
            }
            return res;
        }

        public static string eTransactFeedback(short p)
        {
            string desc = "";
            switch (p)
            {
                case -1:
                    {
                        desc = "unsuccessful transaction";
                    }
                    break;
                case 0:
                    {
                        desc = "Transaction Successful";
                    }
                    break;
                case 1:
                    {
                        desc = "Destination Card Not Found";
                    }
                    break;
                case 2:
                    {
                        desc = "Card Number Not Found";
                    }
                    break;
                case 3:
                    {
                        desc = "Invalid Card PIN";
                    }
                    break;
                case 4:
                    {
                        desc = "Card Expiration Incorrect";
                    }
                    break;
                case 5:
                    {
                        desc = "Insufficient balance";
                    }
                    break;
                case 6:
                    {
                        desc = "Spending Limit Exceeded";
                    }
                    break;
                case 7:
                    {
                        desc = "Internal System Error Occurred, please contact the service provider";
                    }
                    break;
                case 8:
                    {
                        desc = "Financial Institution cannot authorize transaction, Please try later";
                    }
                    break;
                case 9:
                    {
                        desc = "PIN tries Exceeded";
                    }
                    break;
                case 10:
                    {
                        desc = "Card has been locked";
                    }
                    break;
                case 11:
                    {
                        desc = "Invalid Terminal Id";
                    }
                    break;
                case 12:
                    {
                        desc = "Payment Timeout";
                    }
                    break;
                case 13:
                    {
                        desc = "Destination card has been locked";
                    }
                    break;
                case 14:
                    {
                        desc = "Card has expired";
                    }
                    break;
                case 15:
                    {
                        desc = "PIN change required";
                    }
                    break;
                case 16:
                    {
                        desc = "Invalid Amount";
                    }
                    break;
                case 17:
                    {
                        desc = "Card has been disabled";
                    }
                    break;
                case 18:
                    {
                        desc = "Unable to credit this account immediately, credit will be done later";
                    }
                    break;
                case 19:
                    {
                        desc = "Transaction not permitted on terminal";
                    }
                    break;
                case 20:
                    {
                        desc = "Exceeds withdrawal frequency";
                    }
                    break;
                case 21:
                    {
                        desc = "Destination Card has expired";
                    }
                    break;
                case 22:
                    {
                        desc = "Destination Card Disabled";
                    }
                    break;
                case 23:
                    {
                        desc = "Source Card Disabled";
                    }
                    break;
                case 24:
                    {
                        desc = "Invalid Bank Account";
                    }
                    break;
                case 25:
                    {
                        desc = "Insufficient Balance";
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

    }
}
