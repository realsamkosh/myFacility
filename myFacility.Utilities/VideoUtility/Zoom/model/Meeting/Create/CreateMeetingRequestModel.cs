using System;
using System.Collections.Generic;
using System.Text;
using static myFacility.Utilities.VideoUtility.Zoom.Zoomenum;

namespace myFacility.Utilities.VideoUtility.Zoom.model.Meeting.Create
{
    public class CreateMeetingRequestModel
    {
        public string topic { get; set; }
        public int type { get; set; }
        public string start_time { get; set; }
        public int duration { get; set; }
        public string scheduled_for { get; set; }
        public string timezone { get; set; }
        public string password { get; set; }
        public string agenda { get; set; }
        public MeetingSettings settings { get; set; }
        public Recurrence recurrence { get; set; }
    }
}
