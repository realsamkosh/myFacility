using myFacility.Models.Domains.Job;
using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Vendor
{
    public partial class TVendorJobCategoryDoc : BaseObject
    {
        public long MappingId { get; set; }
        public long VendorJobCatId { get; set; }
        public string DocumentName { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string MimeType { get; set; }
        public long TenantId { get; set; }
        public long BranchId { get; set; }
        public long JobCategoryDocId { get; set; }

        public virtual TJobCategoryDocument JobCategoryDoc { get; set; }
        public virtual TVendorJobCategoryMapping VendorJobCat { get; set; }
    }
}
