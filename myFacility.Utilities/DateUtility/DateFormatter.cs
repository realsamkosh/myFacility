using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.DateUtility
{
    public class DateFormatter
    {
        public static string FormatDate_Long(DateTime dt)
        {
            if (dt != null)
            {
                return string.Format("{0:dd MMMM yyyy}", dt);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate_Long1(DateTime dt)
        {
            if (dt != null)
            {
                return string.Format("{0:MMMM dd, yyyy}", dt);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate(DateTime? dt)
        {
            if (dt != null)
            {
                return string.Format("{0:dd-MMM-yyyy}", dt);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate_Appointment(DateTime? dt)
        {
            if (dt != null)
            {
                return dt.Value.ToString("dddd, dd MMMM yyyy");
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate_Short(DateTime? dt)
        {
            if (dt != null)
            {
                return string.Format("{0:dd/MM/yyyy}", dt);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate(DateTime dt)
        {
            if (dt != null)
            {
                return string.Format("{0:dd-MMM-yyyy}", dt);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
