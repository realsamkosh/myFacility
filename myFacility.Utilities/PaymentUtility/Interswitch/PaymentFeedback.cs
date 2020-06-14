using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.PaymentUtility.Interswitch
{
    public class PaymentFeedback
    {
        public static string InterswitchFeedback(string p)
        {
            string desc = "";
            if (p == "00")
                desc = "Approved by Financial Institution";
            else if (p == "01")
                desc = "Refer to Financial Institution";
            else if (p == "02")
                desc = "Refer to Financial Institution, Special Condition";
            else if (p == "03")
                desc = "Invalid Merchant";
            else if (p == "04")
                desc = "Pick-up card";
            else if (p == "05")
                desc = "Do Not Honor";
            else if (p == "06")
                desc = "Error";
            else if (p == "07")
                desc = "Pick-Up Card, Special Condition";
            else if (p == "08")
                desc = "Honor with Identification";
            else if (p == "09")
                desc = "Request in Progress";
            else if (p == "10")
                desc = "Approved by Financial Institution, Partial";
            else if (p == "11")
                desc = "Approved by Financial Institution, VIP";
            else if (p == "12")
                desc = " Invalid Transaction";
            else if (p == "13")
                desc = "Invalid Amount";
            else if (p == "14")
                desc = "Invalid Card Number";
            else if (p == "15")
                desc = "No Such Financial Institution";
            else if (p == "16")
                desc = "Approved by Financial Institution, Update Track 3";
            else if (p == "17")
                desc = "Customer Cancellation";
            else if (p == "18")
                desc = "Customer Dispute";
            else if (p == "19")
                desc = "Re-enter Transaction";
            else if (p == "20")
                desc = "Invalid Response from Financial Institution";
            else if (p == "21")
                desc = "No Action Taken by Financial Institution";
            else if (p == "22")
                desc = "Suspected Malfunction";
            else if (p == "23")
                desc = "Unacceptable Transaction Fee";
            else if (p == "24")
                desc = "File Update not Supported";
            else if (p == "25")
                desc = "Unable to Locate Record";
            else if (p == "26")
                desc = "Duplicate Record";
            else if (p == "27")
                desc = "File Update Field Edit Error";
            else if (p == "28")
                desc = "File Update File Locked";
            else if (p == "29")
                desc = "File Update Failed";
            else if (p == "30")
                desc = "Format Error";
            else if (p == "31")
                desc = "Bank Not Supported";
            else if (p == "32")
                desc = "Completed Partially by Financial Institution";
            else if (p == "33")
                desc = "Expired Card, Pick-Up";
            else if (p == "34")
                desc = "Suspected Fraud, Pick-Up";
            else if (p == "35")
                desc = "Contact Acquirer, Pick-Up";
            else if (p == "36")
                desc = "Restricted Card, Pick-Up";
            else if (p == "37")
                desc = "Call Acquirer Security, Pick-Up";
            else if (p == "38")
                desc = "PIN Tries Exceeded, Pick-Up";
            else if (p == "39")
                desc = "No Credit Account";
            else if (p == "40")
                desc = "Function not Supported";
            else if (p == "41")
                desc = "Lost Card, Pick-Up";
            else if (p == "42")
                desc = "No Universal Account";
            else if (p == "43")
                desc = "Stolen Card, Pick-Up";
            else if (p == "44")
                desc = "No Investment Account";
            else if (p == "51")
                desc = "Insufficient Funds";
            else if (p == "52")
                desc = "No Check Account";
            else if (p == "53")
                desc = "No Savings Account";
            else if (p == "54")
                desc = "Expired Card";
            else if (p == "55")
                desc = "Incorrect PIN";
            else if (p == "56")
                desc = "No Card Record";
            else if (p == "57")
                desc = "Transaction not permitted to Cardholder";
            else if (p == "58")
                desc = "Transaction not permitted on Terminal";
            else if (p == "59")
                desc = "Suspected Fraud";
            else if (p == "60")
                desc = "Contact Acquirer";
            else if (p == "61")
                desc = "Exceeds Withdrawal Limit";
            else if (p == "62")
                desc = "Restricted Card";
            else if (p == "63")
                desc = "Security Violation";
            else if (p == "64")
                desc = "Original Amount Incorrect";
            else if (p == "65")
                desc = "Exceeds withdrawal frequency";
            else if (p == "66")
                desc = "Call Acquirer Security";
            else if (p == "67")
                desc = "Hard Capture";
            else if (p == "68")
                desc = "Response Received Too Late";
            else if (p == "75")
                desc = "PIN tries exceeded";
            else if (p == "76")
                desc = "Reserved for Future Postilion Use";
            else if (p == "77")
                desc = "Intervene, Bank Approval Required";
            else if (p == "78")
                desc = "Intervene, Bank Approval Required for Partial Amount";
            else if (p == "90")
                desc = "Cut-off in Progress";
            else if (p == "91")
                desc = "Issuer or Switch Inoperative";
            else if (p == "92")
                desc = "Routing Error";
            else if (p == "93")
                desc = "Violation of law";
            else if (p == "94")
                desc = "Duplicate Transaction";
            else if (p == "95")
                desc = "Reconcile Error";
            else if (p == "96")
                desc = "System Malfunction";
            else if (p == "98")
                desc = "Exceeds Cash Limit";
            else if (p == "A0")
                desc = "Unexpected error";
            else if (p == "A4")
                desc = "Transaction not permitted to card holder, via channels";
            else if (p == "Z0")
                desc = "Transaction Status Unconfirmed";
            else if (p == "Z1")
                desc = "Transaction Error";
            else if (p == "Z2")
                desc = "Bank account error";
            else if (p == "Z3")
                desc = "Bank collections account error";
            else if (p == "Z4")
                desc = "Interface Integration Error";
            else if (p == "Z5")
                desc = "Duplicate Reference Error";
            else if (p == "Z6")
                desc = "Incomplete Transaction";
            else if (p == "Z7")
                desc = "Transaction Split Pre-processing Error";
            else if (p == "Z8")
                desc = "Invalid Card Number, via channels";
            else if (p == "Z9")
                desc = "Transaction not permitted to card holder, via channels";

            return desc;
        }

    }
}
