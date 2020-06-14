using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.DateUtility
{
    public class DateCalculator
    {

        //Get Month Difference
        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        public static DateTime? GetDateNullValue(Object obj)
        {
            DateTime? retVal = null;
            try
            {
                if (!(obj is DBNull))
                {
                    retVal = (DateTime)obj;
                }
                else
                {
                    retVal = null;
                }
            }
            catch { }
            return retVal;
        }

        public static DateTime GetDateValue(Object obj)
        {
            DateTime retVal = DateTime.Now;
            try
            {
                if (!(obj is DBNull))
                {
                    retVal = (DateTime)obj;
                }
            }
            catch { }
            return retVal;
        }

        public static DateTime ConvertDateValue(Object obj)
        {
            var succeeded = DateTime.TryParse(obj.ToString(), out DateTime result);
            if (succeeded == true)
            {
                return result;
            }
            else
            {
                return DateTime.Now;
            }
        }

        public static DateTime ConvertDateValueWithCulture(String obj)
        {
            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.GetCultureInfo("en-GB");

            var succeeded = DateTime.TryParse(obj, culture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out DateTime result);
            if (succeeded == true)
            {
                return result;
            }
            else
            {
                return DateTime.Now;
            }
        }

        public static string GetDateStringValue(Object obj)
        {
            string retVal = DateTime.Now.ToShortDateString();
            try
            {
                retVal = (!(obj is DBNull) ? ((DateTime)obj).ToShortDateString() : "");
            }
            catch { }
            return retVal;
        }

        public static bool IsDate(String date)
        {
            return DateTime.TryParse(date, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out DateTime temp) &&
                   temp.Hour == 0 &&
                   temp.Minute == 0 &&
                   temp.Second == 0 &&
                   temp.Millisecond == 0 &&
                   temp > DateTime.MinValue;
        }

        public static int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            {
                age -= 1;
            }
            return age;
        }

        /// <summary>  
        /// For calculating age  
        /// </summary>  
        /// <param name="Dob">Enter Date of Birth to Calculate the age</param>  
        /// <returns> years, months,days, hours...</returns>  
        public static string CalculateYourAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            //int Hours = Now.Subtract(PastYearDate).Hours;
            //int Minutes = Now.Subtract(PastYearDate).Minutes;
            //int Seconds = Now.Subtract(PastYearDate).Seconds;
            return string.Format("Age: {0} Year(s) {1} Month(s)",
            Years, Months);
            //return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
            //Years, Months, Days, Hours, Seconds);
        }

        /// <summary>  
        /// For calculating age  
        /// </summary>  
        /// <param name="Dob">Enter Date of Birth to Calculate the age</param>  
        /// <returns> years, months,days, hours...</returns>  
        public static string CalculateYourAgeShort(DateTime Dob)
        {
            string age = string.Empty;
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            if (Years == 0 && Months > 0 && Days == 0)
            {
                return string.Format("{0}Mnth(s)", Months);
            }
            else if (Years == 0 && Months > 0 && Days > 0)
            {
                return string.Format("{0}Mnth(s) {1}Day(s)", Months, Days); ;
            }
            else if (Years > 0 && Months > 0 && Days == 0)
            {
                return string.Format("{0}Yr(s) {1}Mnth(s)", Years, Months);
            }
            else if (Years > 0 && Months == 0 && Days > 0)
            {
                return string.Format("{0}Yr(s) {1}Day(s)", Years, Days);
            }
            else if (Years > 0 && Months == 0 && Days == 0)
            {
                return string.Format("{0}Yr(s)", Years);
            }
            else if (Years == 0 && Months == 0 && Days > 0)
            {
                return string.Format("{0}Day(s)", Days);
            }
            else
            {
                return string.Format("{0}Yr(s)", Years); ;
            }
            //int Hours = Now.Subtract(PastYearDate).Hours;
            //int Minutes = Now.Subtract(PastYearDate).Minutes;
            //int Seconds = Now.Subtract(PastYearDate).Seconds;
        }

        /// <summary>
        /// For calculating Reminder Intervals
        /// </summary>
        /// <param name="CurrentDate"> The current time the Reminder is to be sent</param>
        /// <param name="ApointmentDate">The Patient Appointment Date</param>
        /// <param name="Metrics">The Metrics in Days, Weeks, Months, Hours, Minutes</param>
        /// <param name="Intervals">This is the figure to be compare against</param>
        /// <returns></returns>
        public static bool CalculateReminderIntervals(DateTime CurrentDate, DateTime ApointmentDate, string Metrics, int Intervals)
        {
            bool status = false;

            switch (Metrics)
            {
                case "Days":
                    var checkDays = (CurrentDate - ApointmentDate).TotalDays;
                    if (checkDays >= Intervals)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    break;
                case "Day":
                    var checkDay = (CurrentDate - ApointmentDate).TotalDays;
                    if (checkDay >= Intervals)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    break;
                case "Min":
                    var checkMin = (CurrentDate - ApointmentDate).TotalMinutes;
                    if (checkMin >= Intervals)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    break;
                case "Hr":
                    var checkHr = (CurrentDate - ApointmentDate).TotalHours;
                    if (checkHr >= Intervals)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    break;
                case "days":
                    var checkdays = (CurrentDate - ApointmentDate).TotalDays;
                    if (checkdays >= Intervals)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    break;
                case "day":
                    var checkday = (CurrentDate - ApointmentDate).TotalDays;
                    if (checkday >= Intervals)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    break;
                case "min":
                    var checkmin = (CurrentDate - ApointmentDate).TotalMinutes;
                    if (checkmin >= Intervals)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    break;
                case "hr":
                    var checkhr = (CurrentDate - ApointmentDate).TotalHours;
                    if (checkhr >= Intervals)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    break;
                default:
                    break;
            }
            return status;
        }
    }
}
