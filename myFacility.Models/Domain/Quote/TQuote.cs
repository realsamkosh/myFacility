using myFacility.Models.Domains.Award;
using myFacility.Models.Domains.Job;
using myFacility.Models.Domains.Vendor;
using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Quote
{
    public partial class TQuote : BaseObject
    {
        public TQuote()
        {
            TAwardedJobs = new HashSet<TAwardedJobs>();
            TQuoteKpiLog = new HashSet<TQuoteKpiLog>();
        }

        public long QuoteId { get; set; }
        public long JobId { get; set; }
        public long VendorId { get; set; }
        public string QuoteNo { get; set; }
        public decimal Score { get; set; }
        public bool? IsRated { get; set; }
        public string RatedBy { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }

        public virtual TJob Job { get; set; }
        public virtual TVendorPersonalRegistration Vendor { get; set; }
        public virtual ICollection<TAwardedJobs> TAwardedJobs { get; set; }
        public virtual ICollection<TQuoteKpiLog> TQuoteKpiLog { get; set; }
    }
}
