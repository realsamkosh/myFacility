using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.VideoUtility.Zoom
{
    public class Zoomenum
    {
        public enum GrantType
        {
            authorization_code
        }

        public enum AuthWorkflow
        {
            code
        }

        public enum MeetingType
        {
            Instant = 1,
            Scheduled = 2,
            RecurringNoFixedTime = 3,
            RecurringFixedTime = 8
        }
        public enum ApprovalType
        {
            AutomaticallyApprove,
            ManuallyApprove,
            NoRegistrationRequired
        }

        public enum AutoRecording
        {
            local,
            cloud,
            none
        }

        public enum Audio
        {
            both,
            telephony,
            voip
        }
    }
}
