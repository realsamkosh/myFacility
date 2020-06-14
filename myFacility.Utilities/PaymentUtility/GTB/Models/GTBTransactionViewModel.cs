using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.PaymentUtility.GTB.Models
{
    public class GTBTransactionViewModel
	{
        
	    public long transaction_id { get; set; }
		public string trx_ref { get; set; }
		public string booking_code { get; set; }
		public long user_id { get; set; }
		public decimal transaction_amount { get; set; }
		public decimal technology_fee_amount { get; set; }
		public decimal gateway_amount { get; set; }
		public decimal total_payable_amount { get; set; }
		public int sharing_id { get; set; }
		public string request_payLoad { get; set; }
		public string response_payLoad { get; set; }
		public string response_code { get; set; }
		public string response_description { get; set; }
		public DateTime paymentdate { get; set; }
		public long pyt_status { get; set; }
		public bool iscanceled { get; set; }
		public bool is_active { get; set; }
		public DateTime created_date { get; set; }
		public DateTime last_modified { get; set; }
		public string created_by { get; set; }
		public string modified_by { get; set; }
		public DateTime Script { get; set; }


		public long RecordSaved { get; set; } = 0;

		public string ResponseMessage { get; set; }
	}
}
