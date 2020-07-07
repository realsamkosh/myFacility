using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Job
{
    public partial class TJobJobcategoryMapping
    {
        public long MappingId { get; set; }
        public long JobId { get; set; }
        public long JobCategoryId { get; set; }

        public virtual TJob Job { get; set; }
        public virtual TJobCategory JobCategory { get; set; }
    }
}
