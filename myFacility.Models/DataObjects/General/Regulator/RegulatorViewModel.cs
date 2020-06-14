using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Models.DataObjects.Regulator
{

    public class RegulatorViewModel
    {
        public int id { get; set; }
        public string location { get; set; }
        public string name { get; set; }
        public int? status { get; set; }
        public Guid guid { get; set; }
    }
}
