using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.DataObject.MessageObject
{
    public class ApplicantBroadcastScheduleViewModel
    {
        public string scheduleid { get; set; }
        public string groupid { get; set; }
        public string groupname { get; set; }
        public string broadcasttype { get; set; }
        public string emailtemplateid { get; set; }
        public string emailtemplatename { get; set; }
        public string smstemplateid { get; set; }
        public string smstemplatename { get; set; }
        public string frequencyid { get; set; }
        public string frequency { get; set; }
    }
}
