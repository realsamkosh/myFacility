using myFacility.Utilities.PaymentUtility.GTB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.PaymentUtility.GTB.Interfaces
{
    public interface IGTBPayment
    {
        GTBTransactionViewModel PrepareGTBPayment(GTBTransactionViewModel transactionPayLoad);

       // GTBPaymentTransaction ProcessPayment(GTBPaymentTransaction processPayment);
    }
}
