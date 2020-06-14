using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Payment.Core.Remitta.Constants
{
    public class LineItem
    {
        /// <summary>
        /// Unique identifier for the line items
        /// </summary>
        public string lineItemsId { get; set; }
        /// <summary>
        /// Name of the account
        /// </summary>
        public string beneficiaryName { get; set; }
        /// <summary>
        /// Account number
        /// </summary>
        public string beneficiaryAccount { get; set; }
        /// <summary>
        /// CBN bank code to identify each banks
        /// </summary>
        public string bankCode { get; set; }
        /// <summary>
        /// A percentage of the total amount for the account
        /// </summary>
        public string beneficiaryAmount { get; set; }
        /// <summary>
        /// Specifies the line item Remita is to deduct her total transaction fee for all line items. Either 0 or 1.
        /// </summary>
        public string deductFeeFrom { get; set; }
    }
}
