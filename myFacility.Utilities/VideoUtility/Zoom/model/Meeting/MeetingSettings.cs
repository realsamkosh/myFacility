using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.VideoUtility.Zoom.model.Meeting
{
    public class MeetingSettings
    {
        public bool host_video { get; set; }
        public bool paticipant_video { get; set; }
        public bool cn_meeting { get; set; }
        public bool in_meeting { get; set; }
        public bool join_before_host { get; set; }
        public bool mute_upon_entry { get; set; }
        public bool watermark { get; set; }
        public bool use_pmi { get; set; }
        public int approval_type { get; set; }
        public string audio { get; set; }
        public bool enforce_login { get; set; }
        public string enforce_login_domains { get; set; }
        public string alternative_hosts { get; set; }
        public bool close_registration { get; set; }
        public bool waiting_room { get; set; }
        public List<string> global_dial_in_countries { get; set; }
        public string contact_name { get; set; }
        public string contact_email { get; set; }
        public string auto_recording { get; set; }
        public bool registrants_email_notification { get; set; }
        public bool registrants_confirmation_email { get; set; }
        public bool meeting_authentication { get; set; }
        public string authentication_domains { get; set; }
        public List<GlobalDialInNumbers> global_dial_in_numbers { get; set; }
        public string[] additional_data_center_regions { get; set; }
    }
}
