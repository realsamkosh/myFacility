using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using myFacility.Models.DataObjects.User;
using myFacility.Models.Domains.Account;

namespace myFacility.Services.Contract
{
    public interface IUserManagementServices
    {
        Task<IEnumerable<UserViewModel>> GetUsers(DateTime? startdate = null, DateTime? enddate = null, bool status = true);
        Task<UserViewModel> GetUser(string id, bool status = true);
        //Task<IEnumerable<UserViewModel>> GetAllUsers(bool status = true);
        Task<BtUser> CheckUser(string username);
        Task<string> UpdateMyProfileAsync(UserViewModel obj);
    }
}
