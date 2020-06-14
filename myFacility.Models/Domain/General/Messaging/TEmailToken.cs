using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Messaging
{
    public partial class TEmailToken : BaseObject
    {
        public int EmailtokenId { get; set; }
        public string EmailToken { get; set; }
        public string PreviewName { get; set; }
    }
}
