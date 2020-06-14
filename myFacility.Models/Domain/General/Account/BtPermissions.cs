using myFacility.Models.Domains.Regulator;
using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Account
{
    public partial class BtPermissions
    {
        public string PermissionId { get; set; }
        public string Permissions { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public long RegId { get; set; }
        public bool IsActive { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

        public virtual TRegulator Reg { get; set; }
    }
}
