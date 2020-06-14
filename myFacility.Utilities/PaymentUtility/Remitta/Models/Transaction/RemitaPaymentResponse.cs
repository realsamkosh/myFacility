using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Payment.Core.Remitta.Response
{
    public class RemitaPaymentResponse
    {
        public string statuscode { get; set; }
        public string RRR { get; set; }
        public string status { get; set; }
        public string Hash { get; set; }
        public string MerchantId { get; set; }
    }
}
