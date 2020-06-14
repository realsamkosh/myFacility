using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.VideoUtility.Zoom.model.Authorization.OAuth
{
    public class AuthorizationRequestModel
    {
        public string response_type { get; set; }
        public string client_id { get; set; }
        public string redirect_uri { get; set; }
    }
}
