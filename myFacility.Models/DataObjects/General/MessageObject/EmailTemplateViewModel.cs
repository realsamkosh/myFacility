using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Models.DataObjects.MessageObject
{
    public class EmailTemplateViewModel
    {
        public int templateid { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}
