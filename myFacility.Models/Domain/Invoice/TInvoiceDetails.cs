using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Invoice
{
    public partial class TInvoiceDetails : BaseObject
    {
        public long DetailsId { get; set; }
        public long? InvoiceId { get; set; }
        public string Note { get; set; }
        public string Value { get; set; }
        public decimal? Percentage { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }

        public virtual TInvoice Invoice { get; set; }
    }
}
