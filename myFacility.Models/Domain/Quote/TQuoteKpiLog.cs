using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Quote
{
    public partial class TQuoteKpiLog : BaseObject
    {
        public long LogId { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }
        public long QuoteId { get; set; }
        public long KpiSettingId { get; set; }
        public string Kpi { get; set; }

        public virtual TQuote Quote { get; set; }
    }
}
