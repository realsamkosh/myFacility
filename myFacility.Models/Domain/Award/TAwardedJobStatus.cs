using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Award
{
    public partial class TAwardedJobStatus : BaseObject
    {
        public TAwardedJobStatus()
        {
            TAwardedJobs = new HashSet<TAwardedJobs>();
        }

        public int StatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<TAwardedJobs> TAwardedJobs { get; set; }
    }
}
