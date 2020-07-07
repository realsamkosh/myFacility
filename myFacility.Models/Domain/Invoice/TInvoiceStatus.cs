using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Invoice
{
    public partial class TInvoiceStatus : BaseObject
    {
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}
