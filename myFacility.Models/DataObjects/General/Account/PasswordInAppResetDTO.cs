using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Models.DataObjects.Account
{
    public class PasswordInAppResetDTO
    {
        [Remote(action: "VerifyCuurentPassword", controller: "Json")]
        public string currentpassword { get; set; }
        [Remote(action: "VerifyPassword", controller: "Json", AdditionalFields = nameof(confirmpassword))]
        public string password { get; set; }
        [Remote(action: "VerifyPassword", controller: "Json", AdditionalFields = nameof(password))]
        public string confirmpassword { get; set; }
    }
}
