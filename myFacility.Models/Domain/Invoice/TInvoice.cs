using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Invoice
{
    public partial class TInvoice : BaseObject
    {
        public TInvoice()
        {
            TInvoiceDetails = new HashSet<TInvoiceDetails>();
        }

        public long InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public long AwardedJobInvoiceStatusId { get; set; }
        public long? AwardedJobId { get; set; }
        public long VendorId { get; set; }
        public long? DeptId { get; set; }
        public long? BankId { get; set; }
        public string AccountName { get; set; }
        public long? InvoiceTypeId { get; set; }
        public int? CurrencyId { get; set; }
        public int? SkipJobCompletion { get; set; }
        public bool? IsProcessed { get; set; }
        public string ProcessedBy { get; set; }
        public long? LowValueJobId { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }

        public virtual ICollection<TInvoiceDetails> TInvoiceDetails { get; set; }
    }
}
