using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace myFacility.Models.DataObjects.Account
{
    public class LoginDTO
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
