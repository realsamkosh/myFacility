using myFacility.Models.Domains.Award;
using myFacility.Models.Domains.Quote;
using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Vendor
{
    public partial class TVendorPersonalRegistration : BaseObject
    {
        public TVendorPersonalRegistration()
        {
            TAwardedJobs = new HashSet<TAwardedJobs>();
            TQuote = new HashSet<TQuote>();
            TVendorBusinessRegistration = new HashSet<TVendorBusinessRegistration>();
            TVendorJobCategoryMapping = new HashSet<TVendorJobCategoryMapping>();
            TVendorRejectionHistory = new HashSet<TVendorRejectionHistory>();
        }

        public long PvregId { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }
        public string ApplicantName { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Email { get; set; }
        public string PositionInOrganization { get; set; }
        public string FormNumber { get; set; }
        public string AccountStatus { get; set; }
        public bool IsCreatedByTenant { get; set; }
        public bool? IsProcurement { get; set; }
        public bool? IsSupply { get; set; }
        public string Comment { get; set; }
        public int? StateId { get; set; }

        public virtual ICollection<TAwardedJobs> TAwardedJobs { get; set; }
        public virtual ICollection<TQuote> TQuote { get; set; }
        public virtual ICollection<TVendorBusinessRegistration> TVendorBusinessRegistration { get; set; }
        public virtual ICollection<TVendorJobCategoryMapping> TVendorJobCategoryMapping { get; set; }
        public virtual ICollection<TVendorRejectionHistory> TVendorRejectionHistory { get; set; }
    }
}
