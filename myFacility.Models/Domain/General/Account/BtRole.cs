using Microsoft.AspNetCore.Identity;
using System;

namespace myFacility.Models.Domains.Account
{
    public class BtRole : IdentityRole
    {
        //[Required(AllowEmptyStrings = false)] //A role must have at least one role in it
        //private readonly string _permissionsInRole;
        public BtRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        // public IEnumerable<myFacilityPermissions> PermissionsInRole =>_permissionsInRole.UnpackPermissionsFromString();

        public string PermissionsInRole { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsSuperUser { get; set; }
        public string RoleDesc { get; set; }
        public bool IsActive { get; set; }
        //public virtual ICollection<RolePermissions> RolePermissions { get; set; }
    }
}
