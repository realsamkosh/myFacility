using myFacility.DataObjects.Common;
using myFacility.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace myFacility.Services.Handler
{
    public class CommonServices : ICommonServices
    {
        public void CreateEmailTemplateLkup(EmailTemplateLkupDTO obj)
        {
            throw new NotImplementedException();
        }

        public void CreateQuestionaireTypeLkup(QuestionaireTypeLkupDTO obj)
        {
            throw new NotImplementedException();
        }

        public void CreateQuestionDefnLkup(QuestionDefnLkupDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task CreateReligion(ReligionDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<SmsTemplateLkupViewModel> CreateSmsTemplateLkup(SmsTemplateLkupDTO obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmailTemplateLkup(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuestionaireTypeLkup(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuestionDefnLkup(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteSmsTemplateLkup(string id)
        {
            throw new NotImplementedException();
        }

        public string GenericEncryption(string id)
        {
            throw new NotImplementedException();
        }

        public int GenericRetrievalInt(string id)
        {
            throw new NotImplementedException();
        }

        public long GenericRetrievalLong(string id)
        {
            throw new NotImplementedException();
        }

        public Task<EmailTemplateLkupViewModel> GetEmailTemplateLkup(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmailTemplateLkupViewModel>> GetEmailTemplateLkups(bool status = true)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FrequencyViewModel>> GetFrequencies(bool status = true)
        {
            throw new NotImplementedException();
        }

        public int GetIntIDConverter(string id)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionaireTypeLkupViewModel> GetQuestionaireTypeLkup(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuestionaireTypeLkupViewModel>> GetQuestionaireTypeLkups(bool status = true)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionDefnLkupViewModel> GetQuestionDefnLkup(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuestionDefnLkupViewModel>> GetQuestionDefnLkups(bool status = true)
        {
            throw new NotImplementedException();
        }

        public Task<ReligionViewModel> GetReligion(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReligionViewModel>> GetReligions(bool status = true)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReligionViewModel>> GetReligionsByMerchant(bool status = true)
        {
            throw new NotImplementedException();
        }

        public Task<SmsTemplateLkupViewModel> GetSmsTemplateLkup(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SmsTemplateLkupViewModel>> GetSmsTemplateLkups(bool status = true)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmailTemplateLkup(EmailTemplateLkupDTO obj, string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateQuestionaireTypeLkup(QuestionaireTypeLkupDTO obj, string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateQuestionDefnLkup(QuestionDefnLkupDTO obj, string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReligion(ReligionDTO obj, string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateSmsTemplateLkup(SmsTemplateLkupDTO obj, string id)
        {
            throw new NotImplementedException();
        }
    }
}
