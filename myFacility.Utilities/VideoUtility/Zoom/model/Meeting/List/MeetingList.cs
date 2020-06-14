using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.VideoUtility.Zoom.model.Meeting.List
{
    public class MeetingList
    {
        public int page_count { get; set; }
        public int page_number { get; set; }
        public int page_size { get; set; }
        public int total_records { get; set; }
        public List<Meetings> meetings { get; set; }
    }
}
