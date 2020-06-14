namespace myFacility.Payment.Core.Remitta.Models
{
    public class Payment
    {
        /// <summary>
        /// This is a unique identifier for service type receiving the payment
        /// </summary>
        public string serviceTypeId { get; set; }
        /// <summary>
        /// This is the Biller Transaction ID
        /// </summary>
        public string orderId { get; set; }
        /// <summary>
        /// This is the total monetary value of the transaction.
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// This is the name of the customer to be displayed on the payment page.
        /// </summary>
        public string payerName { get; set; }
        /// <summary>
        /// This is the Payer’s Email Address
        /// </summary>
        public string payerEmail { get; set; }
        /// <summary>
        /// This is the Payer’s Phone Number
        /// </summary>
        public string payerPhone { get; set; }
        /// <summary>
        /// Details of the service your customer is paying for.
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Optional (Used only when the amount is to be split between different accounts)
        /// </summary>
    }
}
