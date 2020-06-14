using System;
using System.Collections.Generic;
using System.Text;
using myFacility.Utilities.VideoUtility.Zoom.model.Initiate.Meeting;

namespace myFacility.Utilities.VideoUtility.Zoom.model.Signature
{
    public class SignatureViewModel : InitiateMeetingViewModel
    {
        public string key { get; set; }
        public string password { get; set; }
        public string meetingnumber { get; set; }
        public string leaveurl { get; set; }
    }
}
