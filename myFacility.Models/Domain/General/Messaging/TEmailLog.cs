using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Messaging
{
    public partial class TEmailLog 
    {
        public long EmaillogId { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Name { get; set; }
        public string EmailCc { get; set; }
        public string EmailBcc { get; set; }
        public bool Sent { get; set; }
        public string Subject { get; set; }
        public bool HasAttachment { get; set; }
        public string AttachmentLoc { get; set; }
        public DateTime Datetosend { get; set; }
        public bool Sendimmediately { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public DateTime? Lastmodified { get; set; }
        public string Modifiedby { get; set; }
        public bool? CanSend { get; set; }
        public bool? FailedSending { get; set; }
        public DateTime? LastFailed { get; set; }
    }
}
