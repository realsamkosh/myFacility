using myFacility.DataObject.SysAdmin.MessageObject;
using myFacility.Models.DataObjects.MessageObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myFacility.Services.Contract
{
    public interface IMessageManagementService
    {
        #region Email Template
        Task<IEnumerable<EmailTemplateViewModel>> GetEmailTemplates(bool status = true);
        Task<EmailTemplateViewModel> GetEmailTemplate(int id, bool status = true);
        Task<string> CreateEmailTemplate(EmailTemplateDTO obj);
        Task<string> UpdateEmailTemplate(EmailTemplateDTO obj, int id);
        Task<bool> DeleteEmailTemplate(int id, bool status);
        #endregion

        #region SMS Template
        Task<IEnumerable<SmsTemplateViewModel>> GetSmsTemplates(bool status = true);
        Task<SmsTemplateViewModel> GetSmsTemplate(int id, bool status = true);
        Task<string> CreateSmsTemplate(SmsTemplateDTO obj);
        Task<string> UpdateSmsTemplate(SmsTemplateDTO obj, int id);
        Task<bool> DeleteSmsTemplate(int id, bool status);
        #endregion
        //Task CreateMessageBroadcast(BroadcastScheduleDTO obj);

        #region Email Token
        Task<IEnumerable<EmailTokensViewModel>> GetEmailTokens(bool status = true);
        Task<IEnumerable<SmsTokensViewModel>> GetSmsTokens(bool status = true);
        #endregion
    }
}
