using myFacility.Payment.Core.Paystack.Models.Charge;
using System.Threading.Tasks;

namespace myFacility.Payment.Core.Paystack.Interfaces
{
    public interface ICharge
    {
        Task<ChargeResponseModel> ChargeCard(string email, string amount, string pin, string cvv,
            string expiry_month, string expiry_year, string number, string display_name = null, string value = null,
            string variable_name = null);

        Task<ChargeResponseModel> SubmitPin(string pin, string reference);

        Task<ChargeResponseModel> SubmitOTP(string otp, string reference);

        Task<ChargeResponseModel> CheckPendingCharge(string reference);
    }
}
