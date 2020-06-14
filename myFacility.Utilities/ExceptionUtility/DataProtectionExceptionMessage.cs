using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.ExceptionUtility
{
    public class DataProtectionExceptionMessage
    {
        public const string InvalidProtectedId = "The provided payload cannot be decrypted because it was not protected with this protection provider.";
    }
}
