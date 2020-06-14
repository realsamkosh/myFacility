using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.Enums
{
   public class GTBPaymentEnum
   {
        public  const long PendingPayment =0;
        public const long Paid = 1;
        public const long RecordCreatedSuccessful = 1;
        public const long RecordCreatedUnsuccessful = 1;
        public const long Naira = 566;
        public const long USD = 826;
        public const string Payment_Description = "Payment for Bus Fare";
        public const string DataIntegrity_OK = "OK";
        public const string DataIntegrity_Fail = "Fail";
    }
}
