using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.SMSUtility.InfoBip.Model
{
    public class InfoBipSMSRequest
    {
        public string from { get; set; }
        public string to { get; set; }
        public string text { get; set; }
    }
}
