//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace myFacility.Services.Contract
//{
//    public interface IPasswordManagementService
//    {
//        //User Default Password Services
//        IEnumerable<TCoreUsersPasswordObject> GetUserDefaultPasswordsById(int? id);
//        IEnumerable<TCoreUsersPasswordObject> GetUserDefaultPasswords(int id);
//        TCoreUsersPasswordObject GetUserDefaultPassword(int? id);
//        TCoreUsersPasswordObject GetUserDefaultPasswordByProfile(int? userId);
//        void IncUserDefaultPasswordInvalidLogin(int? userId, int? Invalid, string UserName);
//        void IncUserDefaultPasswordSuccessfulLogin(TCoreUsersPasswordObject obj);
//        void IncUserDefaultPasswordCumulativeLogin(int? userId, int? Cumulative, string UserName);
//        void CreateUserDefaultPassword(TCoreUsersPasswordObject obj);
//        bool UpdateUserDefaultPassword(TCoreUsersPasswordObject obj);
//        bool MailingPassword(TCoreUsersPasswordObject obj);
//        bool DeleteUserDefaultPassword(TCoreUsersPasswordObject obj);

//        //User Old Password Services
//        IEnumerable<TCoreUsersPasswordHistObject> GetUserOldPasswords(int? id);
//        TCoreUsersPasswordHistObject GetUserOldPassword(int? id);
//        TCoreUsersPasswordHistObject GetUserOldPasswordByProfile(int? userId);
//        void CreateUserOldPassword(TCoreUsersPasswordHistObject obj);
//        bool UpdateUserOldPassword(TCoreUsersPasswordHistObject obj);
//        bool DeleteUserOldPassword(TCoreUsersPasswordHistObject obj);

//        //User Old Password Services
//        IEnumerable<PasswordOptionsObject> GetPasswordOptions();
//        PasswordOptionsObject GetPasswordOption(int? id);
//        bool UpdatePasswordOption(PasswordOptionsObject obj);


//        //Passwrd Enforcement
//        TCoreUsersPasswordObject GetUserDefaultPasswordByUsername(string id);
//    }
//}
