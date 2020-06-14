using myFacility.Payment.Core.Paystack.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using myFacility.Model.DataObjects.General.Payment;

namespace myFacility.Services.Contract
{
    public interface IPaymentService
    {
        Task<PaymentInitalizationResponseModel> InitializePaymentToPayStack(decimal amount);
        string VerifyPayStackPayment(string RefrenceCode);
        Task<IEnumerable<PaymentHistoryViewModel>> GetPaymentHistory();
    }
}
