using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Vendor
{
    public partial class TVendorRejectionHistory : BaseObject
    {
        public long RejectionId { get; set; }
        public long PvregId { get; set; }
        public string Reason { get; set; }

        public virtual TVendorPersonalRegistration Pvreg { get; set; }
    }
}
