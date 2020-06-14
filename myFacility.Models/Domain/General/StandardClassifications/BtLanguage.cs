using myFacility.Models.Domains;

namespace myFacility.Domains.StandardClassifications
{
    public partial class BtLanguage : BaseObject
    {
        public int LangId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
