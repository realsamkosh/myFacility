using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Models.DataObjects.User
{
    public class UserDTO
    {
        public string phonenumber { get; set; }
        public string email { get; set; }
        public int? stateid { get; set; }
        public int? lgaid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        //public string password { get; set; }
        //public string confirmpassword { get; set; }
    }
}
