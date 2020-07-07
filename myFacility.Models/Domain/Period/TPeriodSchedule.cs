using myFacility.Models.Domains.Vendor;
using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Period
{
    public partial class TPeriodSchedule : BaseObject
    {
        public TPeriodSchedule()
        {
            TVendorAppraisal = new HashSet<TVendorAppraisal>();
        }

        public long PeriodId { get; set; }
        public long PeriodTypeId { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? StopDate { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }

        public virtual TPeriodType PeriodType { get; set; }
        public virtual ICollection<TVendorAppraisal> TVendorAppraisal { get; set; }
    }
}
