using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Job
{
    public partial class TLowValueJob : BaseObject
    {
        public long LowvaluejobId { get; set; }
        public long JobTypeId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public int Status { get; set; }
        public string Details { get; set; }
        public string JobNo { get; set; }
        public string LowValueCode { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }
        public long? KpiProfileId { get; set; }

        public virtual TJobType JobType { get; set; }
    }
}
