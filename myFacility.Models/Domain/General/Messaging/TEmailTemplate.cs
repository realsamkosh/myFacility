using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Messaging
{
    public partial class TEmailtemplate : BaseObject
    {
        public int EtemplateId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
