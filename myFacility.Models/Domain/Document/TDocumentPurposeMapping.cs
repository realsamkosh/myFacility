using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Document
{
    public partial class TDocumentPurposeMapping : BaseObject
    {
        public long MappingId { get; set; }
        public long DocumentId { get; set; }
        public int PurposeId { get; set; }

        public virtual TDocument Document { get; set; }
        public virtual TDocumentPurpose Purpose { get; set; }
    }
}
