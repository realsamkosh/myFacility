using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Invoice
{
    public partial class TInvoiceType : BaseObject
    {
        public long TypeId { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }
        public string Name { get; set; }
        public bool CheckJobCompletion { get; set; }
    }
}
