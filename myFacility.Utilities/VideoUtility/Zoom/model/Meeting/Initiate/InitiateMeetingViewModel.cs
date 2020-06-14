using System;
using System.Collections.Generic;
using System.Text;
using myFacility.Utilities.VideoUtility.Zoom.model.Meeting.Join;

namespace myFacility.Utilities.VideoUtility.Zoom.model.Initiate.Meeting
{
    public class InitiateMeetingViewModel : JoinMeetingViewModel
    {
        public bool disablerecord { get; set; }
        public bool disableinvite { get; set; }
        public bool issupportchat { get; set; }
        public bool screenshare { get; set; }
        public bool videodrag { get; set; }
        public bool issupportav { get; set; }
        public bool disablecallout { get; set; }
        public bool disablejoinaudio { get; set; }
        public bool audiopanelalwaysopen { get; set; }
    }
}
