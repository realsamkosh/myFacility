using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace myFacility.Models.DataObjects.Account
{
    public class ReactivationRequestViewModel
    {
        public string userid { get; set; }
        public string reason { get; set; }
        public string status { get; set; }
        public string Requester { get; set; }
        public DateTime StatusChangeDate { get; set; }
        public string ProtectedId { get; set; }
        public User_reactivation_request_status istatus { get; set; }
        public string DeCu { get; set; }
        public string Reasonss { get; set; }
    }
    public enum User_reactivation_request_status
    {
        [Description("Approved")]
        Approved = 'A',
        [Description("Deny")]
        Deny = 'D',
    }
}
