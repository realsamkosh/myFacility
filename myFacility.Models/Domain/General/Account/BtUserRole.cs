using Microsoft.AspNetCore.Identity;
using System;

namespace myFacility.Models.Domains.Account
{
    public class ApplicationUserClaim : IdentityUserClaim<string> { }
    public class ApplicationUserLogin : IdentityUserLogin<string> { }
    public class ApplicationRoleClaim : IdentityRoleClaim<string> { }
    public class BtUserRole : IdentityUserRole<string>
    {
        public BtUserRole()
            : base()
        { }

        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
