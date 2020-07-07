using System;
using System.Collections.Generic;

namespace myFacility.Models.Domains.Document
{
    public partial class TDocumentFormat : BaseObject
    {
        public long DocumentId { get; set; }
        public string Formats { get; set; }
    }
}
