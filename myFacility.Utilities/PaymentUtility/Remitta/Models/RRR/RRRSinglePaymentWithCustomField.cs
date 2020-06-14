using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Payment.Core.Remitta.Models.RRR
{
    public class RRRSinglePaymentWithCustomField : Payment
    {
        public CustomField customFields { get; set; }
    }
}
