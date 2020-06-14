using System;

namespace myFacility.Models.DataObjects.User
{
    public class UserViewModel
    {
        public string appuserid { get; set; }
        public long userid { get; set; }
        public string usernname { get; set; }
        public string usernameucase { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string usercategory { get; set; }
        public string phonenumber { get; set; }
        //[Remote(action: "VerifyEmail", controller: "Validation")]
        public string email { get; set; }
        public bool isactive { get; set; }
        public string country { get; set; }
        public string lga { get; set; }
        public string state { get; set; }
        public int? lgaid { get; set; }
        public int? stateid { get; set; }
        public string FullName
        {
            get
            {
                return String.Format("{0} {1} {2}", this.firstname, this.middlename, this.lastname);
            }
        }
    }
}
