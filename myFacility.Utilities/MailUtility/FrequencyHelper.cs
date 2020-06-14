using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.MailUtility
{
    public class FrequencyHelper
    {
        public static DateTime? CalculateNextBroadcastDate(string frequencycode, DateTime scheduledate)
        {
            DateTime? nextdate = null;
            switch (frequencycode)
            {
                case "I": //this will be sent as soon the schedule is submitted
                    break;
                case "O": //this will be sent once based on seletected
                    nextdate = scheduledate;
                    break;
                case "D":
                    nextdate = scheduledate.AddHours(24);
                    break;
                case "W":
                    nextdate = scheduledate.AddDays(7);
                    break;
                case "M":
                    nextdate = scheduledate.AddMonths(1);
                    break;
                case "Y":
                    nextdate = scheduledate.AddYears(1);
                    break;
                default:
                    break;
            }
            return nextdate;
        }

    }
}
