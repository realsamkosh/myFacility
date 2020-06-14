using myFacility.Payment.Core.Paystack.Models.Subscription;
using System.Threading.Tasks;

namespace myFacility.Payment.Core.Paystack.Interfaces
{
    public interface ISubscriptions
    {
        Task<SubscriptionModel> CreateSubscription(string customerEmail, string planCode, string authorization,string start_date);
    }
}
