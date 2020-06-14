using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Payment.Core.Remitta.Payment.SplitPayments
{
    public class SplitResponseVO
    {
        public string orderId { get; set; }
        public string RRR { get; set; }
        public string status { get; set; }
        public string statuscode { get; set; }
    }
}
