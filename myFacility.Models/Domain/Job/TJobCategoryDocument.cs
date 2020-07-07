using myFacility.Models.Domains.Document;
using myFacility.Models.Domains.Vendor;
using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Job
{
    public partial class TJobCategoryDocument : BaseObject
    {
        public TJobCategoryDocument()
        {
            TVendorJobCategoryDoc = new HashSet<TVendorJobCategoryDoc>();
        }

        public long MappingId { get; set; }
        public long JobCategoryId { get; set; }
        public long DocumentId { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }

        public virtual TDocument Document { get; set; }
        public virtual TJobCategory JobCategory { get; set; }
        public virtual ICollection<TVendorJobCategoryDoc> TVendorJobCategoryDoc { get; set; }
    }
}
