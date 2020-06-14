using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Model.Domain.General.StoredProcedureObject
{
    public class LaboratoryResult
    {
        public int OrderID { get; set; }
        public int OrderDetailID { get; set; }
        public int SubTestID { get; set; }
        public DateTime? OrderCreatedDate { get; set; }
        public string LabNumber { get; set; }
        public string Priority { get; set; }
        public string PatientNo { get; set; }
        public int PatientID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Category { get; set; }
        public int Status { get; set; }
        public string LabTest { get; set; }
        public string ResultValue { get; set; }
        public string Interpretation { get; set; }
        public string Microscopy { get; set; }
        public string RefRange { get; set; }
        public string DoctorName { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ResultDate { get; set; }
        public string Comment { get; set; }
        public DateTime? SampledDate { get; set; }
        public string Pathologist { get; set; }
        public string ResultsPathologist { get; set; }
    }
}
