using myFacility.Utilities.PaymentUtility.GTB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.PaymentUtility.GTB.Interfaces
{
    public class GTBPayment : IGTBPayment
    {
        public GTBTransactionViewModel PrepareGTBPayment(GTBTransactionViewModel transactionPayLoad)
        {
            try
            {
                /// This will save the request into the database before processing of the payment by GTB



                /// genenerate the required model to process the payment

                //GTBPaymentArtifact model = new GTBPaymentArtifact
                //{
                //    mode

                //};
                return null;

            }
            finally
            {

            }
        }

        public GTBPaymentArtifact ProcessPayment()
        {
            try
            {
                // process the payment


                return null;

            }
            finally
            {

            }
        }

        //public GTBPaymentTransaction ProcessPayment(GTBPaymentTransaction processPayment)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
