using System;
using System.Collections.Generic;
using System.Text;
using myFacility.Utilities.VideoUtility.Zoom.model.DefaultHostUser;

namespace myFacility.Utilities.VideoUtility.Zoom.model.Key
{
    public class ZoomKey
    {
        public ZoomAPIKey API { get; set; }
        public ZoomSDKKey SDK { get; set; }
        public DefaultHost DefaultHost { get; set; }
        public string MeetingNumber { get; set; }
        public string LeaveUrl { get; set; }
    }
}
