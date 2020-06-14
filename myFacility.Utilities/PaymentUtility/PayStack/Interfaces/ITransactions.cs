using myFacility.Payment.Core.Paystack.Models;
using myFacility.Payment.Core.Paystack.Models.Authorizations;
using myFacility.Payment.Core.Paystack.Models.Exports;
using myFacility.Payment.Core.Paystack.Models.TransTotal;
using System.Threading.Tasks;

namespace myFacility.Payment.Core.Paystack.Interfaces
{
    public interface ITransactions
    {
        Task<PaymentInitalizationResponseModel> InitializeTransaction(string email, int amount,string firstName = null, 
            string lastName = null, string callbackUrl =null, string reference = null, bool makeReferenceUnique = false);

        Task<PaymentInitalizationResponseModel> InitializeTransaction(TransactionInitializationRequestModel requestObj);

        Task<TransactionResponseModel> VerifyTransaction(string reference);

        Task<TransactionListViewModel> ListTransactions();

        Task<TransactionResponseModel> FetchTransaction(int id);

        Task<TransactionResponseModel> ChargeAuthorization(string authorization_code, string email, int amount);

        Task<TransactionResponseModel> TransactionTimeline(string reference);

        Task<TransactionTotal> TransactionTotals();

        Task<ExportResponseModel> ExportTransactions();

        Task<TransactionResponseModel> CheckAuthorization(string authorization_code, string email, int amount);

        Task<RequestAuthorizationModel> RequestReAuthorization(string authorization_code, string email, int amount);

    }
}
