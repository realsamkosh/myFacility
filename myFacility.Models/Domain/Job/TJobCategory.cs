using myFacility.Models.Domains.KPI;
using myFacility.Models.Domains.Vendor;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Job
{
    public partial class TJobCategory : BaseObject
    {
        public TJobCategory()
        {
            TJobCategoryDocument = new HashSet<TJobCategoryDocument>();
            TJobJobcategoryMapping = new HashSet<TJobJobcategoryMapping>();
            TKpiProfile = new HashSet<TKpiProfile>();
            TVendorJobCategoryMapping = new HashSet<TVendorJobCategoryMapping>();
        }

        public long CategoryId { get; set; }
        public long JobTypeId { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public virtual TJobType JobType { get; set; }
        public virtual ICollection<TJobCategoryDocument> TJobCategoryDocument { get; set; }
        public virtual ICollection<TJobJobcategoryMapping> TJobJobcategoryMapping { get; set; }
        public virtual ICollection<TKpiProfile> TKpiProfile { get; set; }
        public virtual ICollection<TVendorJobCategoryMapping> TVendorJobCategoryMapping { get; set; }
    }
}
