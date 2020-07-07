using myFacility.Models.Domains.Period;
using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Vendor
{
    public partial class TVendorAppraisal : BaseObject
    {
        public TVendorAppraisal()
        {
            TVendorAppraisalKpi = new HashSet<TVendorAppraisalKpi>();
        }

        public long AppraisalId { get; set; }
        public long PeriodTypeId { get; set; }
        public long PeriodId { get; set; }
        public int Year { get; set; }
        public long VendorId { get; set; }
        public decimal AvgQuoteScore { get; set; }
        public decimal AvgJobCompletionScore { get; set; }
        public decimal AvgOtherScore { get; set; }
        public string Comment { get; set; }
        public bool IsSent { get; set; }
        public DateTime? DateSent { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }

        public virtual TPeriodSchedule Period { get; set; }
        public virtual TPeriodType PeriodType { get; set; }
        public virtual ICollection<TVendorAppraisalKpi> TVendorAppraisalKpi { get; set; }
    }
}
