using myFacility.Models.Domains;
using myFacility.Models.Domains.Account;
using System;

namespace myFacility.Models.Domains.Account
{
    public class BtUserReactivation : BaseObject
    {
        public long ReactivateId { get; set; }
        public string UserId { get; set; }
        public long MerchantId { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string Requester { get; set; }
        public DateTime StatusChangeDate { get; set; }

        public virtual BtUser User { get; set; }
    }
}
