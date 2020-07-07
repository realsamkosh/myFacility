using myFacility.Models.Domains.Job;
using myFacility.Models.Domains.Quote;
using myFacility.Models.Domains.Vendor;
using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Award
{
    public partial class TAwardedJobs : BaseObject
    {
        public long AwardedJobId { get; set; }
        public long JobId { get; set; }
        public long VendorId { get; set; }
        public long QuoteId { get; set; }
        public int AwardStatusId { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public DateTime AwardedDate { get; set; }
        public string PoNumber { get; set; }
        public bool? EnableChangeForm { get; set; }

        public virtual TAwardedJobStatus AwardStatus { get; set; }
        public virtual TJob Job { get; set; }
        public virtual TQuote Quote { get; set; }
        public virtual TVendorPersonalRegistration Vendor { get; set; }
    }
}
