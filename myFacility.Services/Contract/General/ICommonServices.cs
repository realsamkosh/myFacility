using myFacility.DataObjects.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myFacility.Services.Contract
{
    public interface ICommonServices
    {
        #region Religion Interface
        /// <summary>
        /// Get All Religions
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ReligionViewModel>> GetReligions(bool status = true);
        /// <summary>
        /// Get all Religions by Merchant id
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ReligionViewModel>> GetReligionsByMerchant(bool status = true);
        Task<ReligionViewModel> GetReligion(string id);
        Task CreateReligion(ReligionDTO obj);
        Task UpdateReligion(ReligionDTO obj, string id);
        int GetIntIDConverter(string id);

        #endregion

        //#region Policy Acceptance Page
        //Task<IEnumerable<UserLicenseAgreementViewModel>> GetUserLicenseAgreements(bool status = true);
        //Task<UserLicenseAgreementViewModel> GetUserLicenseAgreement(string id);
        //void CreateUserLicenseAgreement(UserLicenseAgreementDTO obj);
        //void UpdateUserLicenseAgreement(UserLicenseAgreementDTO obj, string Id);
        //void DeleteUserLicenseAgreement(string id);
        //#endregion

        #region Email Template Lookup Interface
        Task<IEnumerable<EmailTemplateLkupViewModel>> GetEmailTemplateLkups(bool status = true);
        Task<EmailTemplateLkupViewModel> GetEmailTemplateLkup(string id);
        void CreateEmailTemplateLkup(EmailTemplateLkupDTO obj);
        void UpdateEmailTemplateLkup(EmailTemplateLkupDTO obj, string id);
        void DeleteEmailTemplateLkup(string id);
        #endregion

        #region SMS Template Lookup Interface
        Task<IEnumerable<SmsTemplateLkupViewModel>> GetSmsTemplateLkups(bool status = true);
        Task<SmsTemplateLkupViewModel> GetSmsTemplateLkup(string id);
        Task<SmsTemplateLkupViewModel> CreateSmsTemplateLkup(SmsTemplateLkupDTO obj);
        void UpdateSmsTemplateLkup(SmsTemplateLkupDTO obj, string id);
        void DeleteSmsTemplateLkup(string id);
        #endregion

        #region QuestionaireTypeLkup Interface
        Task<IEnumerable<QuestionaireTypeLkupViewModel>> GetQuestionaireTypeLkups(bool status = true);
        Task<QuestionaireTypeLkupViewModel> GetQuestionaireTypeLkup(string id);
        void CreateQuestionaireTypeLkup(QuestionaireTypeLkupDTO obj);
        void UpdateQuestionaireTypeLkup(QuestionaireTypeLkupDTO obj, string id);
        void DeleteQuestionaireTypeLkup(string id);
        #endregion

        #region QuestionDefnLkup Interface
        Task<IEnumerable<QuestionDefnLkupViewModel>> GetQuestionDefnLkups(bool status = true);
        Task<QuestionDefnLkupViewModel> GetQuestionDefnLkup(string id);
        void CreateQuestionDefnLkup(QuestionDefnLkupDTO obj);
        void UpdateQuestionDefnLkup(QuestionDefnLkupDTO obj, string id);
        void DeleteQuestionDefnLkup(string id);
        #endregion

        #region Frequency Interface
        Task<IEnumerable<FrequencyViewModel>> GetFrequencies(bool status = true);
        #endregion


        #region Generic Encryption and Decryption
        int GenericRetrievalInt(string id);
        long GenericRetrievalLong(string id);
        string GenericEncryption(string id);

        #endregion

      
    }
}
