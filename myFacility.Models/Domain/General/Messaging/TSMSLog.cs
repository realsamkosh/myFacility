using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Model.Domain.General.Messaging
{
    public class TSmsLog
    {
        public long SmslogId { get; set; }
        public string Message { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Name { get; set; }
        public bool Sent { get; set; }
        public string Subject { get; set; }
        public DateTime Datetosend { get; set; }
        public bool Sendimmediately { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public DateTime? Lastmodified { get; set; }
        public string Modifiedby { get; set; }
        public bool? CanSend { get; set; }
        public bool? FailedSending { get; set; }
        public DateTime? LastFailed { get; set; }
        public string ErrorMessage { get; set; }
    }
}
