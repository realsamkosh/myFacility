using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Models.Domains.Account
{
    public class BtUserLoginHistory
    {
        public string LoginhistId { get; set; }
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public DateTime SessionDate { get; set; }
        public string Operation { get; set; }
        public string Browser { get; set; }
    }
}
