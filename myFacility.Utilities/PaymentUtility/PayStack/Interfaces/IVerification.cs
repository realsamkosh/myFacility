using myFacility.Payment.Core.Paystack.Models.Verification;
using System.Threading.Tasks;

namespace Paystack.NetmyFacility.Payment.Core.Paystack.Interfaces
{
    public interface IVerification
    {
        Task<BVNVerificationResponseModel> ResolveBVN(string bvn);
    }
}
