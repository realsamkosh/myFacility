using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Vendor
{
    public partial class TVendorAppraisalKpi : BaseObject
    {
        public long KpiId { get; set; }
        public long AppraisalId { get; set; }
        public string Kpi { get; set; }
        public decimal Weight { get; set; }
        public decimal Score { get; set; }

        public virtual TVendorAppraisal Appraisal { get; set; }
    }
}
