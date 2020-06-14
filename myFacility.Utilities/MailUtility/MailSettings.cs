using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.MailUtility
{
    public class MailSettings
    {
        /// <summary>
        /// Email Port Number
        /// </summary>
        public bool EnableSsl { get; set; }
        public int DeliveryMethod { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPHost { get; set; }
        public string Password { get; set; }
        public string BCC { get; set; }
        public string CC { get; set; }
    }
}
