using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.PaymentUtility.GTB.Interfaces
{
   public class GTBPaymentArtifact
    {
        public string gtpay_mert_id { get; set; }
        public string gtpay_tranx_id { get; set; }

        public decimal gtpay_tranx_amt { get; set; }

        public string gtpay_tranx_curr { get; set; }

        public string gtpay_cust_id { get; set; }
        public string gtpay_tranx_memo { get; set; }

        public string gtpay_tranx_noti_url { get; set; }

        public string gtpay_gway_name { get; set; }

        public string gtpay_gway_first { get; set; }

        public string gtpay_echo_data { get; set; }

        public string gtpay_cust_name { get; set; }

        public string gtpay_hash { get; set; }

        public string DataIntegrity { get; set; }
        public string ResponseMessage { get; set; }
    }
}
