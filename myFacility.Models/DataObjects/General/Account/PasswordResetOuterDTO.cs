using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Models.DataObjects.Account
{
    public class PasswordResetOuterDTO
    {
        public string code { get; set; }
        public string userid { get; set; }
        [Remote(action: "VerifyPassword", controller: "Json", AdditionalFields = nameof(confirmpassword))]
        public string password { get; set; }
        [Remote(action: "VerifyPassword", controller: "Json", AdditionalFields = nameof(password))]
        public string confirmpassword { get; set; }
    }
}
