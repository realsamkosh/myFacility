using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.VideoUtility.Zoom.model
{
    public class BaseConstants
    {
        public const string ZoomBaseEndPoint = "https://api.zoom.us/";
        public const string ZoomOAuth2BaseEndPoint = "https://zoom.us/oauth/";
        public const string ZoomOAuth2AccessTokenRequestBaseEndPoint = "token";
        public const string ZoomOAuth2UserAuthorizationBaseEndPoint = "authorize";

        public const string ZoomMeetingCreateEndPoint = "v2/users/{0}/meetings";
        public const string ZoomMeetinggGetAllMeetingEndPoint = "v2/users/{0}/meetings";
    }
}
