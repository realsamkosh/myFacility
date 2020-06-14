using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Models.Domains.Account
{
    public class BTUsersPasswordHist
    {
        public string HistryId { get; set; }
        public long EmpId { get; set; }
        public int PwdCount { get; set; }
        public string PwdEncrypt { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }

       // public virtual EmployeeDomain.BtEmployee Emp { get; set; }
    }
}
