using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myFacility.Infrastructure;
using myFacility.Models.DataObjects.User;
using myFacility.Models.Domains.Account;
using myFacility.Services.Contract;
using myFacility.Utilities.AuthenticationUtility.AuthUser;
using myFacility.Utilities.ExceptionUtility;

namespace myFacility.Services.Handler
{
    public class UserManagementServices : IUserManagementServices
    {
        private authDbContext _context;
        private IDataProtector _protector;
        private IConfiguration _configura;
        private readonly UserManager<BtUser> _userManager;
        private readonly RoleManager<BtRole> _roleManager;
        private readonly IAuthUser _authUser;
        private readonly IMailManagementService _mailService;
        private readonly ILogger<UserManagementServices> _logger;

        public UserManagementServices(authDbContext context,
            IDataProtectionProvider dataProtectionProvider,
            IConfiguration configura, UserManager<BtUser> userManager,
            RoleManager<BtRole> roleManager, IAuthUser authUser,
            ILogger<UserManagementServices> logger,
            IMailManagementService mailService)
        {
            _context = context;
            _protector = dataProtectionProvider.CreateProtector(GetType().FullName);
            _configura = configura;
            _userManager = userManager;
            _roleManager = roleManager;
            _authUser = authUser;
            _mailService = mailService;
            _logger = logger;
        }


        #region All Users Services
        public async Task<IEnumerable<UserViewModel>> GetUsers(DateTime? startdate = null, DateTime? enddate = null, bool status = true)
        {
            try
            {
                if (startdate == null && enddate == null)
                {
                    return await _context.Users
                        .Where(x => x.UserName != "admin")
                        .Select(x => new UserViewModel
                        {
                            appuserid = x.Id,
                            email = x.Email,
                            //firstname = x.Firstname,
                            //isactive = x.IsActive.Value,
                            //lastname = x.Lastname,
                            //middlename = x.Middlename,
                            usercategory = x.UserCategory,
                            usernname = x.UserName,
                            //userid = x.UserextId,
                            phonenumber = x.PhoneNumber,
                            //state = x.StateId == null ? "NIL" : x.State.Name,
                            //lga = x.LgaId == null ? "NIL" : x.Lga.Name,
                            //lgaid = x.LgaId,
                            //stateid = x.StateId,
                        }).ToListAsync();
                }
                else
                {
                    return await _context.Users
                        .Where(x => x.UserName != "admin")
                        .Select(x => new UserViewModel
                        {
                            appuserid = x.Id,
                            email = x.Email,
                            //firstname = x.Firstname,
                            //isactive = x.IsActive.Value,
                            //lastname = x.Lastname,
                            //middlename = x.Middlename,
                            usercategory = x.UserCategory,
                            usernname = x.UserName,
                            //userid = x.UserextId,
                            phonenumber = x.PhoneNumber,
                            //state = x.StateId == null ? "NIL" : x.State.Name,
                            //lga = x.LgaId == null ? "NIL" : x.Lga.Name,
                            //lgaid = x.LgaId,
                            //stateid = x.StateId,
                        }).ToListAsync();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<IEnumerable<UserViewModel>> GetAllUsers(bool status = true)
        //{
        //    try
        //    {
        //        return await _context.TUserExt.Include(x => x.Gsuser)
        //            .Where(x => x.IsActive == status)
        //            .Select(x => new UserViewModel
        //            {
        //                appuserid = x.Gsuser.Id,
        //                email = x.Gsuser.Email,
        //                firstname = x.Firstname,
        //                isactive = x.IsActive.Value,
        //                lastname = x.Lastname,
        //                middlename = x.Middlename,
        //                usercategory = x.UserCategory,
        //                usernname = x.Gsuser.UserName,
        //                userid = x.UserextId,
        //                //ProtectId = x,
        //            }).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<UserViewModel> GetUser(string id, bool status = true)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    return await _context.Users
                     .Where(x => x.UserName != "admin" && x.Id == id)
                     .Select(x => new UserViewModel
                     {
                         appuserid = x.Id,
                         email = x.Email,
                         phonenumber = x.PhoneNumber,
                         //firstname = x.Firstname,
                         //isactive = x.IsActive.Value,
                         //lastname = x.Lastname,
                         //middlename = x.Middlename,
                         usercategory = x.UserCategory,
                         usernname = x.UserName,
                         //userid = x.UserextId,
                         //lgaid = x.LgaId,
                         //stateid = x.StateId
                     }).FirstOrDefaultAsync();

                }
                else
                {
                    throw new InvalidOperationException($"Id - {id} doesn't exist in the system!");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<BtUser> CheckUser(string username)
        {
            try
            {
                var fetchrecord = await _context.Users.FirstOrDefaultAsync(x => x.Email == username || x.UserName == username);
                if (fetchrecord != null)
                {
                    return fetchrecord;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region User
        public async Task<string> UpdateMyProfileAsync(UserViewModel obj)
        {
            try
            {
                var checkuser = await _context.Users.FirstOrDefaultAsync(x => x.Id != _authUser.UserId);
                if (checkuser.Email == obj.email)
                {
                    return string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.email);
                }

                if (checkuser.PhoneNumber == obj.phonenumber)
                {
                    return string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.phonenumber);
                }

                //var fetchdata = await _context.TUserExt.FirstOrDefaultAsync(x => x.GsuserId == _authUser.UserId);
                var fetchuser = await _context.Users.FirstOrDefaultAsync(x => x.Id == _authUser.UserId);

                fetchuser.Email = obj.email;
                fetchuser.NormalizedEmail = obj.email.ToUpper();
                fetchuser.PhoneNumber = obj.phonenumber;
                //fetchdata.Firstname = obj.firstname;
                //fetchdata.LastModified = DateTime.Now;
                //fetchdata.Lastname = obj.lastname;
                //fetchdata.Middlename = obj.middlename;
                //fetchdata.ModifiedBy = _authUser.Name;
                if (await _context.SaveChangesAsync() > 0)
                {
                    return "1";
                }
                return ResponseErrorMessageUtility.RecordNotSaved;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        #endregion
    }
}

