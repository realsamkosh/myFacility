using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Payment.Core.Remitta.Bank
{
    public class RemittaBank
    {
        public int bankid { get; set; }
        public string bankname { get; set; }
        public string bankcode { get; set; }
        public int createdby { get; set; }
        public int isactive { get; set; }
    }
}
