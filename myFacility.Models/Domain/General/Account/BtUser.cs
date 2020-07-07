using Microsoft.AspNetCore.Identity;
using System;

namespace myFacility.Models.Domains.Account
{
    public class BtUser : IdentityUser
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public bool HasBranch { get; set; }
        public DateTime PwdExpiryDate { get; set; }
        public DateTime? PwdChangedDate { get; set; }
        public bool ForcePwdChange { get; set; }
        public DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UserCategory { get; set; }
        //public bool IsPrivacyPolicyAgreed { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string MiddleName { get; set; }
    }
}
