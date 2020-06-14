using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlTypes;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace myFacility.Utilities.ExceptionUtility
{
    public class ResponseErrorMessageUtility
    {
        public const string NotExistProtectedId = "Operation Failed. The {0} supplied is not valid or does not exist.";
        public const string NotExistRecord = "Operation Failed. The {0} provided does not exist on the system.";
        public const string NotValidProtectedId = "Operation Failed. The {0} supplied is not valid.";
        public const string NotFoundMerchantUser = "Your merchant record cannot be found.";
        public const string CurrentBranchName = "This is your current branch.";
        public const string BranchSelection = "Set your Branch to continue";
        public const string PermissionCheck = "This is record is system generated records(s). You do not have the permission to edit this record. Only User-defined record(s) can be edited";

        /// <summary>
        /// {0} - Record Column Name
        /// </summary>
        public const string RecordExistBefore = "The {0} record you are trying to create already exists on the system.";
        public const string RecordExistBeforeII = "The {0} record you are trying to {1} already exists on the system.";
        public const string RecordExistBeforeDeactivated = "The {0} record you are trying to create already exists but deactivated on the system. Contact your administrator.";
        public const string RecordAttachedToOthers = "The {0} record you are trying to delete has an attached {1} on the system.";
        public const string NotAuthorizedUser = "Your request cannot be processed. Please Login to Continue";
        public const string RecordNotFound = "The record does not exist";
        public const string OperationFailed = "The operation was not successful.";
        public const string TripTypeNotDetermined = "The Trip Type Could not be determined";


        /// <summary>
        /// {0} - Record Column Name
        /// {1} - Incoming Record Taken
        /// </summary>
        public const string TakenRecord = "{0} : {1} is already taken";
        public const string SelectRecord = "Please select {0} from the dropdown list";
        public const string CannotCreateMoreThanOneRecord = "You cannot create more than one record on this system";
        public const string RecordNotSaved = "Operation Failed. We encountered an error saving your data on the system";
        public const string RecordSaved = "The new record was successfully created";
        public const string SavedAndUpdated = "1";
        public const string RecordNotFetched= "Operation Failed. We encountered an error fetching your data on the system";
        public const string LoginUsernamePasswordNotEmpy = "Username or Password cannot be empty";
        public const string PasswordNotMatch = "Password and Confirm Password do not match";
        public const string InvalidLoginAttempt = "Invalid login attempt";
        public const string LockedOutUser = "User account locked out.";
        public const string StatusBool = "Status can only be true or false";
        public const string MappingRecordCannotBeEmpty = "Operation Failed. {0} or Associated {1} cannot be empty";
        public const string InvalidToken = "Invalid token";
        public const string InvalidUser = "Invalid user";
        public const string CannotContainPassword = "Your password cannot contain element of {0}";
        public const string EmptyFormFile = "FormFile is Empty.";
        public const string SupportedFileFormat = "The system currently supports only {0} format.";
        public const string RequiredColumnsFromTemplate = "Operation Failed. Ensure that the column arrangement conforms to the provided template";
        public const string DuplicateRecordFromFile = "There are duplicate records in the file uploaded as follows \n {0}";
        public const string PlanBenefitOperationFailed = "Operation Failed. Unable to {0} associated services to the plan coverage";
        public const string BenefitCoveredAlreadyPlan = "The Service Type benefit you are trying to create is already covered by the plan.";
        public const string BenefitCoveredAlreadyPlanDeactivated = "The Service Type benefit you are trying to create is already covered by the plan but deactivated.";
        public const string GroupMappingRemoveOperationFailed = "Operation Failed. Unable to Remove associated {0} from group.";

        public const string DataIntegrity_OK = "OK";
        public const string DataIntegrity_Fail = "Fail";
        public const string RecordFetched = "The system was able to fetch this record successfully.";
        public const string UpdateRecord = "The request to update an entity was successful";
        public const string PatientAlreadyCheckedIn = "Patient Already Checked In";
        public const string Successful = "1";


        #region Wallet
        public const string WalletCreated  = "New wallet created ";
        public const string FundWallet = "Your wallet was funded successfully with {0}. Your current balance is {1}";
        public const string DebitWallet = "Your wallet was debited successfully with {0}. Your current balance is {1}";
        public const string UnableToFundWallet = "Your wallet was not funded, you current balance is {0}.";
        public const string UnableToDebitWallet = "Your wallet was not debited, you current balance is {0}.";

        public const string Ininsufficientfunds = "Your currently have insufficient funds in your wallet. Your wallet balance is {0}";

        public const string WalletNotExisting = "The wallet information not found.";
        public const int CreditOperation = 1;
        public const int DebitOperation = 0;




        #endregion
        public const string MessageLength = "{0} should be minimum of {1} and maximum of {2}";
        public static int HandleDatabaseUpdateException(Exception ex)
        {
            int? err = null;
            if (ex is DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException != null && dbUpdateException.InnerException.InnerException != null)
                {
                    if (dbUpdateException.InnerException.InnerException is SqlException sqlException)
                    {
                        switch (sqlException.Number)
                        {
                            case 2627:  // Unique constraint error
                                err = 2627;
                                break;
                            case 547:   // Constraint check violation, Conflict in the database
                                err = 547;
                                break;
                            case 2601:  // Duplicated key row error // Constraint violation exception // A custom exception of yours for concurrency issues           
                                 err = 2601;
                                break;
                            default:
                                err = 0;
                                break;
                                // A custom exception of yours for other DB issues
                                // throw new DatabaseAccessException(dbUpdateException.Message, dbUpdateException.InnerException);
                        }
                    }
                }
            }
            if (ex is DbUpdateConcurrencyException concurrencyException)
            {

            }
            return err.Value;
        }

    }
}
