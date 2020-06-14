using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.AuthenticationUtility.UserAgent
{
    public class UserAgentModel
    {
        private string _userAgent;

        private ClientBrowser _browser;
        public ClientBrowser Browser
        {
            get
            {
                if (_browser == null)
                {
                    _browser = new ClientBrowser(_userAgent);
                }
                return _browser;
            }
        }

        private ClientOS _os;
        public ClientOS OS
        {
            get
            {
                if (_os == null)
                {
                    _os = new ClientOS(_userAgent);
                }
                return _os;
            }
        }

        public UserAgentModel(string userAgent)
        {
            _userAgent = userAgent;
        }
    }
}
