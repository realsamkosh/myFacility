using myFacility.Models.Domains.Award;
using myFacility.Models.Domains.Quote;
using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Job
{
    public partial class TJob : BaseObject
    {
        public TJob()
        {
            TAwardedJobs = new HashSet<TAwardedJobs>();
            TJobJobcategoryMapping = new HashSet<TJobJobcategoryMapping>();
            TQuote = new HashSet<TQuote>();
        }

        public long JobId { get; set; }
        public long JobTypeId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public int Status { get; set; }
        public string JobNo { get; set; }
        public string Details { get; set; }
        public string JobAttachmentFileName { get; set; }
        public long KpiProfileId { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }
        public string SapGroupNo { get; set; }
        public string SapNo { get; set; }
        public long RequestingDeptId { get; set; }

        public virtual TJobType JobType { get; set; }
        public virtual ICollection<TAwardedJobs> TAwardedJobs { get; set; }
        public virtual ICollection<TJobJobcategoryMapping> TJobJobcategoryMapping { get; set; }
        public virtual ICollection<TQuote> TQuote { get; set; }
    }
}
