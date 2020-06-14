using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFacility.Payment.Core.Paystack.Models
{
    public class PaymentInitalizationResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public SubData data { get; set; }
    }

    public class SubData
    {
        public string authorization_url { get; set; }
        public string access_code { get; set; }
        public string reference { get; set; }
        public string status { get; set; }
        public string transaction_date { get; set; }
        public string amount { get; set; }
        public string gateway_response { get; set; }
    }
}
