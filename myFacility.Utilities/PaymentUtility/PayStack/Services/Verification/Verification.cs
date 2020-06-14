using myFacility.Payment.Core.Paystack.Models.Verification;
using Newtonsoft.Json;
using Paystack.NetmyFacility.Payment.Core.Paystack.Interfaces;
using System.Threading.Tasks;

namespace myFacility.Payment.Core.Services.Verification
{
    public class Verification : IVerification
    {
        string _secretKey;
        public Verification(string secretKey)
        {
            this._secretKey = secretKey;
        }

        public async Task<BVNVerificationResponseModel> ResolveBVN(string bvn)
        {
            var client = HttpConnection.CreateClient(this._secretKey);
            var response = await client.GetAsync($"bank/resolve_bvn/{bvn}");

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<BVNVerificationResponseModel>(json);
        }
    }
}
