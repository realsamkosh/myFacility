using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Model.DataObjects.General.Mail
{
    public class ZoomMeetingNotificationDTO
    {
        public string patientname { get; set; }
        public string doctorname { get; set; }
        public string meetingurl { get; set; }
        public string doctorsemail { get; set; }
    }
}
