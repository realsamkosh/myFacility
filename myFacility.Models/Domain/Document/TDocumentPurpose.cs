using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Document
{
    public partial class TDocumentPurpose : BaseObject
    {
        public TDocumentPurpose()
        {
            TDocumentPurposeMapping = new HashSet<TDocumentPurposeMapping>();
        }

        public int PurposeId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<TDocumentPurposeMapping> TDocumentPurposeMapping { get; set; }
    }
}
