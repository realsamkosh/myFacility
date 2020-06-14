using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Messaging
{
    public partial class TSmsToken : BaseObject
    {
        public int SmstokenId { get; set; }
        public string SmsToken { get; set; }
        public string PreviewName { get; set; }
    }
}
