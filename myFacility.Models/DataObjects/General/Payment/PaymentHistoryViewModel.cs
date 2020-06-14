using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Model.DataObjects.General.Payment
{
    public class PaymentHistoryViewModel
    {
        public string trxref { get; set; }
        public string trxdesc { get; set; }
        public long stagingid { get; set; }
        public string transactiondate { get; set; }
        public bool status { get; set; }
    }
}
