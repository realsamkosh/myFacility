using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFacility.Payment.Core.Paystack.Models
{
    public class TransactionListViewModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<Data> data { get; set; }
    }


}
