using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Payment.Core.Remitta.Response
{
    public class RemitaPayNotificationResponse
    {
        public string channel { get; set; }
        public string rrr { get; set; }
        public string amount { get; set; }
        public string debitdate { get; set; }
        public string transactiondate { get; set; }
        public string bank { get; set; }
        public string branch { get; set; }
        public string serviceTypeId { get; set; }
        public string dateRequested { get; set; }
        public string orderRef { get; set; }
        public string payerName { get; set; }
        public string payerPhoneNumber { get; set; }
        public string payerEmail { get; set; }
        public string uniqueIdentifier { get; set; }
        public string orderID { get; set; }
    }
}
