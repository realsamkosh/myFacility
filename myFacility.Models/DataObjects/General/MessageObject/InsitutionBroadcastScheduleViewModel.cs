using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.DataObject.SysAdmin.MessageObject
{
    public class InsitutionBroadcastScheduleViewModel
    {
        public string patientgroupid { get; set; }
        public string patientgroupname { get; set; }
        public string broadcasttype { get; set; }
        public string emailtemplateid { get; set; }
        public string emailtemplatename { get; set; }
        public string smstemplateid { get; set; }
        public string smstemplatename { get; set; }
        public string frequencyid { get; set; }
        public string frequency { get; set; }
    }
}
