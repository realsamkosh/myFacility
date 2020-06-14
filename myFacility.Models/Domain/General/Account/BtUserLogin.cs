using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Account
{
    public partial class BtUserLogin
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string Discriminator { get; set; }
        public string ProviderDisplayName { get; set; }
        public string UserId { get; set; }

        public virtual BtUser User { get; set; }
    }
}
