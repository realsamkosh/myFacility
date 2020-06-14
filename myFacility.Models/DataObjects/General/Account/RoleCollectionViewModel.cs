using myFacility.Models.DataObjects.User;
using System.Collections.Generic;

namespace myFacility.Models.DataObjects.Account
{
    public class RoleCollectionViewModel
    {
        public string roleid { get; set; }
        public string rolename { get; set; }
        public string roledesc { get; set; }

        public IList<UserViewModel> assignedusers { get; set; }
        public IList<UserViewModel> unassignedusers { get; set; }
    }
}
