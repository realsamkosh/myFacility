using System;
using System.Collections.Generic;

namespace myFacility.Domain.General.Payment
{
    public partial class TTransactionLog
    {
        public long TransactionId { get; set; }
        public string TrxRef { get; set; }
        public string TrxDesc { get; set; }
        public long StagingId { get; set; }
        public string UserId { get; set; }
        public bool PytConfirmed { get; set; }
        public bool Iscanceled { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
