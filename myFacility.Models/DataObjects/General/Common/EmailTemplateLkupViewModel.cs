namespace myFacility.DataObjects.Common
{
    public class EmailTemplateLkupViewModel
    {
        public string etemplateid { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public bool? isreplicable { get; set; }
    }
}
