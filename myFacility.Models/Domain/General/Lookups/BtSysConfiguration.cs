using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Lookups
{
    public partial class BtSysConfiguration
    {
        public int ConfigId { get; set; }
        public string DefaultValue { get; set; }
        public string ConfigValue { get; set; }
        public byte Enabled { get; set; }
        public string ConfigValueDesc { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
