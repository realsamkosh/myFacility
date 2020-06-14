using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Models.DataObjects.Account
{
    public class UserLoginHistoryViewModel
    {
        public string loginhistid { get; set; }
        public string userfullname { get; set; }
        public string ipaddress { get; set; }
        public DateTime sessiondate { get; set; }
        public string operation { get; set; }
        public string browser { get; set; }
    }
}
