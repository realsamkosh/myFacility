using System.Collections.Generic;

namespace myFacility.Models.Domains.Location
{
    public partial class BtCountry:BaseObject
    {
        public BtCountry()
        {
            BtState = new HashSet<BtState>();
        }

        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public string NationalCurrency { get; set; }
        public string Nationality { get; set; }

        public virtual ICollection<BtState> BtState { get; set; }
    }
}
