using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Job
{
    public partial class TJobType
    {
        public TJobType()
        {
            TJob = new HashSet<TJob>();
            TJobCategory = new HashSet<TJobCategory>();
            TLowValueJob = new HashSet<TLowValueJob>();
        }

        public long JobtypeId { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TJob> TJob { get; set; }
        public virtual ICollection<TJobCategory> TJobCategory { get; set; }
        public virtual ICollection<TLowValueJob> TLowValueJob { get; set; }
    }
}
