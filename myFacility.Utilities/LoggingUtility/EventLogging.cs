using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.LoggingUtility
{
    public class EventLogging
    {
        public const int GENERATE_ITEMS = 1000;
        public const int LIST_ITEMS = 1001;
        public const int GET_ITEM = 1002;

        public const int INSERT_ITEM = 1003;
        public const string INSERTION_SUCCESS_MESSAGE = "Insertion was sucessful for {0}";

        public const int UPDATE_ITEM = 1004;
        public const string MODIFICATION_SUCCESS_MESSAGE = "Modification was sucessful for {0}";

        public const int DELETE_ITEM = 1005;
        public const string DELETION_SUCCESS_MESSAGE = "Deletion was sucessful for {0}";

        public const int WARNING_DELETE_ITEM = 1006;
        public const string DELETION_WARNING_MESSAGE = "Deletion not completed for {0}, due to restriction usage on the system";

        public const int GET_ITEM_NOTFOUND = 4000;
        public const int UPDATE_ITEM_NOTFOUND = 4001;
        public const int DELETE_ITEM_NOTFOUND = 4002;

        public const int ERROR_ON_INSERTION_CODE = 30001;
        public const string ERROR_ON_INSERTION_MESSAGE = "Insertion was unsucessful due to server error.";

        public const int ERROR_ON_MAIL_SENT_CODE = 50001;
        public const string ERROR_ON_MAIL_SENT_MESSAGE = "Due to network unavailability, the email notification could not be sent at this time to {0}";

        public const int ERROR_ON_INVALID_LOGIN_ATTEMPT_CODE = 2001;
        public const string ERROR_ON_INVALID_LOGIN_ATTEMPT_MESSAGE = "Invalid login attempt.please try again! {0}";

        public const int SUCCESS_ACTIVATION_CODE = 2002;
        public const string SUCCESS_ACTIVATION_MESSAGE = "Activation was successful for {0}";

        public const int SUCCESS_DEACTIVATION_CODE = 2005;
        public const string SUCCESS_DEACTIVATION_MESSAGE = "Deactivation was successful for {0}";

        public const int ERROR_ACTIVATION_CODE = 2003;
        public const string ERROR_ACTIVATION_MESSAGE = "Activation was unsuccessful for {0}";

        public const int ERROR_DEACTIVATION_CODE = 2004;
        public const string ERROR_DEACTIVATION_MESSAGE = "De-activation was unsuccessful for {0}";

        public const int BAD_REQUEST_CODE = 2004;
        public const string BAD_REQUEST_MESSAGE = "BAD REQUEST";

        public const int UPDATE_ERROR_ITEM_CODE = 1004;
        public const string UPDATE_ERROR_MESSAGE = "Modification was failed for {0}";

        public const int ERROR_REQUEST_SUBMISSION_CODE = 4006;
        public const string ERROR_REQUEST_SUBMISSION_MESSAGE = "{0} could not be inserted due to non-existense of endpoint receiver";
    }
}
