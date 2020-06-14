using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.DateUtility
{
    public class DateStringFormat
    {
        public List<string> ShortDateFormats()
        {
            List<string> formats = new List<string>
            { "dd/MM/yyyy","dd-MM-yyyy","yyyy-MM-dd","yyyy/MM/dd"};
            return formats;
        }
    }
}
