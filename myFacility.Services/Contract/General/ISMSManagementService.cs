using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using myFacility.Utilities.SMSUtility;

namespace myFacility.Services.Contract
{
    public interface ISMSManagementService
    {
        SMSSettings GetSmsCredentials();
        Task SendBatchSMSAsync();
        Task SendSMSEmployeeMessageBroadcast();
        Task SendSMSPatientMessageBroadcast();

        #region Specific Functions
        Task SendPatientWelcomeSMS(string url, string PatientNo, string Fullname, string PhoneNumber);
        Task SendPasswordResetSMS(string phonenumber, string username, string reseturl, long merchantid);
        #endregion

        #region Appointment SMS
        //Task SendAppointmentRemindersSMS();
        Task SendAppointmentBookingSms(long merchantid, string fullname,
            string hospital, string phonenumber, string appointdate, string specialist);
        Task SendAppointmentReschedulingSms(long merchantid, string fullname,
            string hospital, string phonenumber, string nextdate, string appoiintmentdate,
            string specialist = null, string clinic = null);
        Task SendAppointmentCancelationSms(long merchantid, string fullname,
            string hospital, string phonenumber, string appointmentdate);
        #endregion
    }
}
