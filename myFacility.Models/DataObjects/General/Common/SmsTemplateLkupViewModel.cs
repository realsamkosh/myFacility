namespace myFacility.DataObjects.Common
{
    public class SmsTemplateLkupViewModel
    {
        public string smstemplateid { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string sender { get; set; }
        public string message { get; set; }
        public bool? isreplicable { get; set; }
    }
}
