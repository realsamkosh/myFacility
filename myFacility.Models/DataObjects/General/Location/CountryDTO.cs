using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Models.DataObjects.Location
{
    public class CountryDTO
    {
        /// <summary>
        /// Country code
        /// </summary>
        public string countrycode { get; set; }
        public string name { get; set; }
        public string nationalcurrency { get; set; }
        public string nationality { get; set; }
    }
}
