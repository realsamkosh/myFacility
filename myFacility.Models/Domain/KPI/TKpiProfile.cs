using myFacility.Models.Domains.Job;
using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.KPI
{
    public partial class TKpiProfile : BaseObject
    {
        public long ProfileId { get; set; }
        public string Name { get; set; }
        public long KpiTypeId { get; set; }
        public long? JobCategoryId { get; set; }

        public virtual TJobCategory JobCategory { get; set; }
    }
}
