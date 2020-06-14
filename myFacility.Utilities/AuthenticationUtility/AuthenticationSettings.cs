using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.AuthenticationUtility
{
    public class AuthenticationSettings
    {
        public string Secret { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public int JwtExpires { get; set; }
    }
}
