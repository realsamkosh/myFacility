using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.DataObject.MessageObject
{
    public class ApplicantBroadcastScheduleDTO
    {
        public string employeegroupid { get; set; }
        public string broadcasttype { get; set; }
        public string emailtemplateid { get; set; }
        public string smstemplateid { get; set; }
        public string frequencyid { get; set; }
    }
}
