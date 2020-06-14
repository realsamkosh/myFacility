using myFacility.Payment.Core.Paystack.Models.Transfers.Initiation;
using myFacility.Payment.Core.Paystack.Models.Transfers.Recipient;
using myFacility.Payment.Core.Paystack.Models.Transfers.TransferDetails;
using System.Threading.Tasks;

namespace myFacility.Payment.Core.Paystack.Interfaces
{
    public interface ITransfers
    {
        Task<TransferRecipientModel> CreateTransferRecipient(string type,string name,string account_number,
                                        string bank_code,string currency = "NGN",string description =null);

        Task<TransferRecipientListModel> ListTransferRecipients();

        Task<TransferInitiationModel> InitiateTransfer(int amount, string recipient_code, string source = "balance", string currency = "NGN", string reason = null);

        Task<TransferDetailsModel> FetchTransfer(string transfer_code);

        Task<TransferDetailsListModel> ListTransfers();

        Task<string> FinalizeTransfer(string transfer_code, string otp);
    }
}
