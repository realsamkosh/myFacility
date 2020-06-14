using System.Collections.Generic;

namespace myFacility.Models.Domains.Location
{
    public partial class BtLga : BaseObject
    {
        public int LgaId { get; set; }
        public string LgaCode { get; set; }
        public string Name { get; set; }
        public string StateCode { get; set; }

        public virtual BtState StateCodeNavigation { get; set; }
    }
}
