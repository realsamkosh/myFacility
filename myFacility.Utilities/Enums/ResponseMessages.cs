using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.Enums
{
    public class ResponseMessages
    {
        public const string SuccessfulMessage = "Your record was created successful";
        public const string SuccessfulUpdate = "Your update request was successful";
        public const string UnSuccessfulUpdate = "Your update request was not successful";
        public const string Duplicate = "An existing record exist, please try again";
        public const string UnexistingRecord = "The requested record don't exist, please try again";
        public const string SystemError = "An error occurred while performing your request ";
        public const string FailedOperation = "Your request was not successful ";
    }
}
