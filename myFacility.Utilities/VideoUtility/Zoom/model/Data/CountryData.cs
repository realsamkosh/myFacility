using System;
using System.Collections.Generic;
using System.Text;
using myFacility.Utilities.VideoUtility.Zoom.model.Meeting;

namespace myFacility.Utilities.VideoUtility.Zoom.model.Data
{
    public class CountryData
    {
        public List<GlobalDialInNumbers> GlobalDialInNumbers()
        {
            List<GlobalDialInNumbers> DialingList = new List<GlobalDialInNumbers>
            {
                new GlobalDialInNumbers { city = "New York", country = "US", country_name = "US", numbers= "+1 1000200200",type= "toll" },
                new GlobalDialInNumbers { city = "San Jose", country = "US", country_name = "US", numbers= "+1 6699006833",type= "toll" },
                new GlobalDialInNumbers { city = "San Jose", country = "US", country_name = "US", numbers= "+1 408000000",type= "toll" },
            };
            return DialingList;
        }

        public List<string> GlobalDialInCountries()
        {
            List<string> DialListCountry = new List<string>
            {
                "US"
            };
            return DialListCountry;
        }
    }
}
