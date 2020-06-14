using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.VideoUtility.Zoom.model.Meeting.Create
{
    public class Recurrence
    {
        public int type { get; set; }
        public int repeat_interval { get; set; }
        public string weekly_days { get; set; }
        public int monthly_day { get; set; }
        public int monthly_week { get; set; }
        public int monthly_week_day { get; set; }
        public int end_times { get; set; }
        public string end_date_time { get; set; }
    }
}
