using myFacility.Models.Domains.Vendor;
using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Period
{
    public partial class TPeriodType : BaseObject
    {
        public TPeriodType()
        {
            TPeriodSchedule = new HashSet<TPeriodSchedule>();
            TVendorAppraisal = new HashSet<TVendorAppraisal>();
        }

        public long PreriodTypeId { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TPeriodSchedule> TPeriodSchedule { get; set; }
        public virtual ICollection<TVendorAppraisal> TVendorAppraisal { get; set; }
    }
}
