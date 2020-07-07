using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Vendor
{
    public partial class TVendorBusinessRegistration : BaseObject
    {
        public long BvregId { get; set; }
        public long PvregId { get; set; }
        public string CompanyName { get; set; }
        public string RegOfficeAddress { get; set; }
        public string ContactAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteAddress { get; set; }
        public bool CanVisit { get; set; }
        public string NewCompanyName { get; set; }
        public int? BankId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }

        public virtual TVendorPersonalRegistration Pvreg { get; set; }
    }
}
