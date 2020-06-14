using myFacility.Models.Domains.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using myFacility.Model.DataObjects.General.Mail;

namespace myFacility.Services.Contract
{
    public interface IMailManagementService
    {
        Task<int> SendWelcomeEmail(string emailadd, string username, string password, string loginurl,string fullname);
        Task<int> SendPasswordResetEmail(string emailadd, string username, string reseturl, long merchantid);
        Task<int> NotifyDoctorMeetingLink(ZoomMeetingNotificationDTO model);
        Task<TEmailtemplate> GetSystemEmailTemplate(string code);
        Task<int> SendAppointmentBooking(PatientAppointmentMailViewModel model);
        Task<int> SendDoctorAppointmentBooking(PatientAppointmentMailViewModel model);
        Task<int> SendApplicationLiveEmail(string emailadd, string loginurl, string firstname, string merchantFullname);
        Task SendBatchMailAsync();
    }
}
