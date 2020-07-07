using myFacility.Models.Domains.Job;
using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Document
{
    public partial class TDocument : BaseObject
    {
        public TDocument()
        {
            TDocumentPurposeMapping = new HashSet<TDocumentPurposeMapping>();
            TJobCategoryDocument = new HashSet<TJobCategoryDocument>();
        }

        public long DocumentId { get; set; }
        public int MaxFileSize { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? HasExpiringDate { get; set; }
        public DateTime? ExpiringDate { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }
        public bool? IsSupply { get; set; }
        public bool? IsProcurement { get; set; }

        public virtual ICollection<TDocumentPurposeMapping> TDocumentPurposeMapping { get; set; }
        public virtual ICollection<TJobCategoryDocument> TJobCategoryDocument { get; set; }
    }
}
