using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Model.DataObjects.General.Mail
{
    public class PatientAppointmentMailViewModel
    {
        public string patientname { get; set; }
        public string scheduleddate { get; set; }
        public string timeslot { get; set; }
        public string doctorname { get; set; }
        public string hospitalname { get; set; }
        public string hospitalcontact { get; set; }
        public string hospitaladdress { get; set; }
        public string emailaddress { get; set; }
        public string patientphonenumber { get; set; }
        public string doctorsemail { get; set; }
    }
}
