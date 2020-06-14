using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Payment.Core.Remitta.Transaction
{
    public class ResponseQuery
    {
        /// <summary>
        /// This uniquely identifies the biller
        /// </summary>
        public string merchantId { get; set; }
        /// <summary>
        /// The Remita Retrieval Reference
        /// </summary>
        public string RRR { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public string transactiontime { get; set; }
        public string orderId { get; set; }
        public string amount { get; set; }
        public string paymentDate { get; set; }
    }
}
