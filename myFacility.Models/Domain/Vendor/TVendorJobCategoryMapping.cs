using myFacility.Models.Domains.Job;
using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Vendor
{
    public partial class TVendorJobCategoryMapping : BaseObject
    {
        public TVendorJobCategoryMapping()
        {
            TVendorJobCategoryDoc = new HashSet<TVendorJobCategoryDoc>();
        }

        public long MappingId { get; set; }
        public long? PvregId { get; set; }
        public long? JobCategoryId { get; set; }
        public string ApprovalStatus { get; set; }

        public virtual TJobCategory JobCategory { get; set; }
        public virtual TVendorPersonalRegistration Pvreg { get; set; }
        public virtual ICollection<TVendorJobCategoryDoc> TVendorJobCategoryDoc { get; set; }
    }
}
