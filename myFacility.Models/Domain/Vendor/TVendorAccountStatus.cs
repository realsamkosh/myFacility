using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Vendor
{
    public partial class TVendorAccountStatus : BaseObject
    {
        public string StatusCode { get; set; }
        public string Description { get; set; }
    }
}
