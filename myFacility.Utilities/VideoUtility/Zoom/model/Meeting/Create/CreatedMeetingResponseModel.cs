using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.VideoUtility.Zoom.model.Meeting.Create
{
    public class CreatedMeetingResponseModel
    {
        public string topic { get; set; }
        public int type { get; set; }
        public int duration { get; set; }
        public string scheduled_for { get; set; }
        public string timezone { get; set; }
        public string password { get; set; }
        public string agenda { get; set; }
        public string start_time { get; set; }
        public string created_at { get; set; }
        public string host_id { get; set; }
        public long id { get; set; }
        public MeetingSettings settings { get; set; }
        public string start_url { get; set; }
        public string status { get; set; }
        public string uuid { get; set; }
    }
}
