using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.KPI
{
    public partial class TKpiType
    {
        public long KpiTypeId { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
