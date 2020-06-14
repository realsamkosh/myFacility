using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
//using static myFacility.Utilities.GensysisEnums;

namespace myFacility.DataObject.MessageObject
{
    public class BroadcastScheduleDTO
    {
        public string messagegroup { get; set; }
        public string templateid { get; set; }
        public string dateofschedule { get; set; }
        public string frequencycode { get; set; }
        //[Required(ErrorMessage = "Message Type is required")]
        //public BroadcastType broadcasttype { get; set; }
        //[Required(ErrorMessage = "Receiver Type ID is required")]
        //public RecieverType receivertype { get; set; }
    }
}
