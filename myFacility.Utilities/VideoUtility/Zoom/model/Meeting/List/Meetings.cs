using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.VideoUtility.Zoom.model.Meeting.List
{
    public class Meetings
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string host_id { get; set; }
        public string topic { get; set; }
        public int type { get; set; }
        public string start_time { get; set; }
        public int duration { get; set; }
        public string timezone { get; set; }
        public string created_at { get; set; }
        public string join_url { get; set; }
    }
}
