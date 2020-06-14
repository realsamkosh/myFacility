using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using myFacility.Infrastructure;
using myFacility.Models.DataObjects.Account;
using myFacility.Models.DataObjects.User;
using myFacility.Models.Domains.Account;
using myFacility.Services.Contract;
using myFacility.Utilities.AuthenticationUtility;
using myFacility.Utilities.AuthenticationUtility.AuthUser;
using myFacility.Utilities.Enums;
using myFacility.Utilities.ExceptionUtility;
using myFacility.Utilities.Extensions.Permission;

namespace myFacility.Services.Handler
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly RoleManager<BtRole> _roleManager;
        private readonly UserManager<BtUser> _userManager;
        private readonly authDbContext _context;
        private readonly IAuthUser _authUser;
        private readonly ILogger<AccountManagementService> _logger;
        private readonly IDataProtector _protector;
        private readonly IHttpContextAccessor _accessor;

        public AccountManagementService(RoleManager<BtRole> roleManager, UserManager<BtUser> userManager,
            ILogger<AccountManagementService> logger, authDbContext context, IAuthUser authUser,
            IHttpContextAccessor accessor, IDataProtectionProvider dataProtectionProvider)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _authUser = authUser;
            _logger = logger;
            _protector = dataProtectionProvider.CreateProtector(GetType().FullName);
            _accessor = accessor;
        }

        #region Authentication
        public Task<SignInResult> AuthenticateUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<string> Confirmations(string username)
        {
            throw new NotImplementedException();
        }

        public Task<string> ConfirmEmail(string userid, string code)
        {
            throw new NotImplementedException();
        }

        public async Task CreateLastLoginDate(string userid)
        {
            try
            {
                var checkexits = await _context.Users.FirstOrDefaultAsync(x => x.Id == userid);
                checkexits.LastLoginDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task CreateLoginHistory(BtUserLoginHistory obj)
        {
            try
            {
                _context.BtUserLoginHistory.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public Task<bool> CreateReactivationRequest(ReactivationRequestDTO obj)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Role Service
        public async Task<string> CreateRoleAsync(RoleDTO model)
        {
            string status = "";
            try
            {
                //Check if Role exist
                var checkexist = await _context.Roles.AnyAsync(x => x.Name == model.rolename);
                if (checkexist == false)
                {
                    BtRole newrole = new BtRole
                    {
                        Name = model.rolename,
                        NormalizedName = model.rolename.ToUpper().Replace(" ", ""),
                        IsSuperUser = false,
                        RoleDesc = model.roledesc,
                        CreatedBy = _authUser.Name,
                        CreatedDate = DateTime.Now,
                        LastModified = null,
                        ModifiedBy = null,
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };

                    var result = await _roleManager.CreateAsync(newrole);

                    if (result.Succeeded)
                    {
                        status = "1";
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, model.rolename);
                return status;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.Select(x => new RoleViewModel
            {
                roleid = x.Id,
                rolename = x.Name,
                roledesc = x.RoleDesc
            }).ToListAsync();
        }


        public async Task<RoleCollectionViewModel> GetAssignedAndUnAssignedUserToRole(string Id)
        {
            RoleCollectionViewModel rolecolllection = new RoleCollectionViewModel();
            try
            {


                rolecolllection.roleid = Id;
                rolecolllection.rolename = _context.Roles.FirstOrDefault(x => x.Id == Id).Name;

                //UnAssigned Users Across the User Table
                List<UserViewModel> list1 = new List<UserViewModel>();
                var userjoining = await (from alluser in _context.Users 
                                         where alluser.Id != _authUser.UserId
                                         select alluser).ToListAsync();

                var rolemembers = (from appusers in userjoining
                                   where !(from userrole in _context.UserRoles
                                           where userrole.RoleId == Id
                                           select userrole.UserId).Contains(appusers.Id)
                                   select appusers).ToList();
                foreach (var item in rolemembers)
                {
                    UserViewModel entity1 = new UserViewModel
                    {
                        //appuserid = item.GsuserId,
                        //lastname = item.Lastname,
                        //firstname = item.Firstname,
                        //usercategory = item.UserCategory,
                        //middlename = item.Middlename
                    };
                    list1.Add(entity1);
                }
                rolecolllection.unassignedusers = list1.ToList();

                //Get Users on the system that are Assigned to Role
                List<UserViewModel> list2 = new List<UserViewModel>();
                var rolemembers2 = (from user in _context.Users
                                    join uroles in _context.UserRoles on user.Id equals uroles.UserId
                                    where uroles.RoleId == Id
                                    select user).ToList();
                foreach (var item2 in rolemembers2)
                {
                    UserViewModel entity1 = new UserViewModel
                    {
                        //appuserid = item2.GsuserId,
                        //lastname = item2.Lastname,
                        //firstname = item2.Firstname,
                        //middlename = item2.Middlename,
                        usercategory = item2.UserCategory
                    };
                    list2.Add(entity1);
                }
                rolecolllection.assignedusers = list2.ToList();
                return rolecolllection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public async Task<RoleViewModel> GetRole(string id)
        {
            try
            {
                return await _roleManager.Roles.Select(x => new RoleViewModel
                {
                    roleid = x.Id,
                    rolename = x.Name,
                    roledesc = x.RoleDesc
                }).FirstOrDefaultAsync(x => x.roleid == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public string GetRoleIDConverter(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateRoleAsync(RoleDTO obj, string id)
        {
            string status = "";
            try
            {
                //Check if record exist not as same id
                var checkexist = await _context.Roles
                  .AnyAsync(x => x.Id != id &&
                   (x.Name == obj.rolename));
                if (checkexist == false)
                {
                    var state = await _context.Roles
                   .FirstOrDefaultAsync(x => x.Id == id);
                    state.Name = obj.rolename;
                    state.RoleDesc = obj.roledesc;
                    state.ModifiedBy = _authUser.Name;
                    state.LastModified = DateTime.Now;
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        status = "1";
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.rolename);
                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }
        public Task DeleteRoleAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> AssignUsersToRole(string roleId, string[] userIds)
        {
            string status = "";
            try
            {
                //First Prepare entry
                var data = userIds.Select(x => new BtUserRole
                {
                    RoleId = roleId,
                    UserId = x,
                }).ToList();

                //Check if UserRole exist
                var checkexist = await _context.UserRoles.Select(x => new BtUserRole
                {
                    RoleId = roleId,
                    UserId = x.UserId,
                }).ToListAsync();

                var final = data.Except(checkexist);
                if (final.Count() > 0)
                {
                    var prepfinal = final.Select(x => new BtUserRole
                    {
                        RoleId = roleId,
                        UserId = x.UserId,
                        LastModified = DateTime.Now,
                        ModifiedBy = _authUser.Name,
                    });

                    _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
                    _context.UserRoles.AddRange(data);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        status = "1";
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordNotFound);
                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }

        public async Task<string> RemoveUsersFromRole(string roleId, string[] userIds)
        {
            string status = "";
            try
            {
                if (userIds.Count() > 0)
                {
                    var data = userIds.Select(x => new BtUserRole
                    {
                        RoleId = roleId,
                        UserId = x,

                    }).ToList();
                    _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
                    _context.UserRoles.RemoveRange(data);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        status = "1";
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordNotFound);
                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }

        public Task MoveUsersToNewRoleAsync(string NewRoleId, string[] userIds)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Permission
        public async Task<string> AddPermissionsToRole(string roleId, string[] permissionId)
        {
            string status = "";
            try
            {
                if (string.IsNullOrEmpty(roleId))
                {
                    status = string.Format(ResponseErrorMessageUtility.NotExistProtectedId, "Role");
                }

                //Instantiate New List of permission Ids
                List<myFacilityPermissions> mappingDTO = new List<myFacilityPermissions>();
                foreach (var item in permissionId)
                {

                    bool success = Enum.TryParse(item, out myFacilityPermissions result);
                    if (success)
                    {
                        mappingDTO.Add(result);
                    }
                    else
                    {
                        status = string.Format(ResponseErrorMessageUtility.NotExistProtectedId, "Permissions");
                    }
                }
                //Check the role if exist permissions

                //Check if the Permissions in Role is Null or has values
                var checkexist = await _context.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
                var checkresult = await GetPermissionsInRole(roleId);
                if (checkresult != null)
                {
                    //Merged the Existing Permissions with the newly added permissions
                    var additionalperms = checkresult.Union(mappingDTO).PackPermissionsIntoString();

                    checkexist.Id = roleId;
                    checkexist.PermissionsInRole = additionalperms;
                    await _context.SaveChangesAsync();
                    return "1";
                }
                else
                {
                    checkexist.Id = roleId;
                    checkexist.PermissionsInRole = mappingDTO.PackPermissionsIntoString();
                    await _context.SaveChangesAsync();
                    return "1";
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }
        private async Task<IEnumerable<myFacilityPermissions>> GetPermissionsInRole(string roleid)
        {
            try
            {

                var result =  _context.Roles.Where(x => x.Id == roleid).AsQueryable();

                //Check if the Permissions in Role is Null or has values
                var checkresult = await result.FirstOrDefaultAsync();
     
                if (checkresult.PermissionsInRole != null)
                {
                    var fetchfromresult = await result.Select(x => x.PermissionsInRole.UnpackPermissionsFromString()).ToListAsync();
                    //Because the permissions are packed we have to put these parts of the query after the ToListAsync()
                    return fetchfromresult.SelectMany(x => x).Distinct();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<string> CalcPermissionsForUserAsync(string userId)
        {
            try
            {
                var permissionforuser = (await (from roles in _context.Roles
                                                join userroles in _context.UserRoles on roles.Id equals userroles.RoleId
                                                where userroles.UserId == userId
                                                select roles.PermissionsInRole.UnpackPermissionsFromString()).ToListAsync())
                //Because the permissions are packed we have to put these parts of the query after the ToListAsync()
                .SelectMany(x => x).Distinct();
                return permissionforuser.PackPermissionsIntoString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return string.Empty;
            }
        }
        public async Task<PermissionCollectionViewModel> GetMerchantPermissionsByRole(string id)
        {
            try
            {
                PermissionCollectionViewModel viewModel = new PermissionCollectionViewModel();

                //This gets all the permissions, with a distinct to remove duplicates

                var getassignedData = await GetPermissionsInRole(id);

                // Using Null Propagation
                if (getassignedData == null)
                {
                    var res = new List<PermissionViewModel>
                    {
                       new PermissionViewModel { permissionid = "",  permissionaccess = "", description = "" },
                    };
                    viewModel.assignedpermissions = res;

                }
                else
                {
                    var finalresult = getassignedData?.Select(b => new PermissionViewModel
                    {
                        permissionid = b.ToString(),
                        permissionaccess = b.GetAttribute<DisplayAttribute>().Name,
                        description = b.GetAttribute<DisplayAttribute>().Description,
                    }).ToList();
                    viewModel.assignedpermissions = finalresult;
                }




                //Get Merchant Packed Permissions
                var merchantid = await _context.TRegulator.FirstOrDefaultAsync();
                //var fetchuser = await dbcontext.Users.FirstOrDefaultAsync(x => x.Id == _authUser.UserId);
                var regid = merchantid.RegId;
                var fetchresult = await GetPackedRegulatorPermissions(regid);

                //Get The Un-assigned Permission to Module
                var getUnassignedData = getassignedData != null
                          ? (from apppermission in fetchresult
                             where
                             !(from rolepermission in getassignedData select rolepermission.ToString())
                             .Contains(apppermission.ToString())
                             select apppermission).Select(b => new PermissionViewModel
                             {
                                 permissionid = b.ToString(),
                                 permissionaccess = b.GetAttribute<DisplayAttribute>().Name,
                                 description = b.GetAttribute<DisplayAttribute>().Description,
                             }).ToList()
                           : fetchresult.Select(x => new PermissionViewModel
                           {
                               permissionid = x.ToString(),
                               permissionaccess = x.GetAttribute<DisplayAttribute>().Name,
                               description = x.GetAttribute<DisplayAttribute>().Description,
                           }).ToList();
                viewModel.unassignedpermissions = getUnassignedData;



                var fetchrecord = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
                viewModel.roleid = id;
                viewModel.rolename = fetchrecord.Name;
                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<PermissionViewModel> GetPermission(string permissionId)
        {
            try
            {
                return await Task.Run(() =>
                {
                    //Uprotect the Encrypted ID
                    var protId = _protector.Unprotect(permissionId);

                    //Convert the Id to string
                    var permissionid = Convert.ToString(protId);
                    var output = Enum.TryParse(permissionid, out myFacilityPermissions result);
                    if (output == true)
                    {
                        var enums = Enum.GetValues(typeof(myFacilityPermissions)).Cast<myFacilityPermissions>();
                        return enums.Where(x => x.ToString() == permissionid).Select(x => new PermissionViewModel
                        {
                            permissionid = _protector.Protect(x.ToString()),
                            permissionaccess = x.GetAttribute<DisplayAttribute>().Name,
                            description = x.GetAttribute<DisplayAttribute>().Description,

                        }).FirstOrDefault();
                    }
                    _logger.LogError(string.Format(ResponseErrorMessageUtility.NotExistProtectedId, "permission"));
                    return null;
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<IEnumerable<PermissionViewModel>> GetPermissions()
        {
            return await Task.Run(() =>
            {
                var enums = Enum.GetValues(typeof(myFacilityPermissions)).Cast<myFacilityPermissions>();
                return enums.Where(x => x.ToString() != "NotSet").Select(x => new PermissionViewModel
                {
                    permissionid = _protector.Protect(x.ToString()),
                    permissionaccess = x.GetAttribute<DisplayAttribute>().Name,
                    description = x.GetAttribute<DisplayAttribute>().Description,

                }).ToList();
            });
        }
        public async Task<IEnumerable<PermissionViewModel>> GetUserPermissions()
        {
            try
            {
                return await Task.Run(() =>
                {
                    var packedPermissions = _accessor.HttpContext.User?.Claims
                    .SingleOrDefault(x => x.Type == PermissionConstants.PackedPermissionClaimType);

                    var fetchpermissionw = packedPermissions?.Value.UnpackPermissionsFromString().Select(x => x.ToString());

                    var enums = Enum.GetValues(typeof(myFacilityPermissions)).Cast<myFacilityPermissions>();
                    return enums.Where(x => fetchpermissionw.Any(p => x.ToString() == p.ToString())).Select(x => new PermissionViewModel
                    {
                        permissionid = x.ToString(),
                        permissionaccess = x.GetAttribute<DisplayAttribute>().Name,
                        description = x.GetAttribute<DisplayAttribute>().Description,

                    }).ToList();
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        private async Task<IEnumerable<myFacilityPermissions>> GetPackedRegulatorPermissions(long merchantid)
        {
            try
            {
                var result = await _context.BtPermissions
                    .Where(x => x.RegId == merchantid)
                    .Select(x => x.Permissions.UnpackPermissionsFromString()).ToListAsync();
                //Because the permissions are packed we have to put these parts of the query after the ToListAsync()
                return result.SelectMany(x => x).Distinct();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<string> RemovePermissionsFromRole(string roleId, string[] permissionId)
        {
            string status = "";
            try
            {
                if (string.IsNullOrEmpty(roleId))
                {
                    status = string.Format(ResponseErrorMessageUtility.NotExistProtectedId, "Role");
                }


                //Instantiate New List of Product Type Module Mapping
                List<myFacilityPermissions> mappingDTO = new List<myFacilityPermissions>();

                foreach (var item in permissionId)
                {
                    bool success = Enum.TryParse(item, out myFacilityPermissions result);
                    if (success)
                    {
                        mappingDTO.Add(result);
                    }
                    else
                    {
                        status = string.Format(ResponseErrorMessageUtility.NotExistProtectedId, "Permissions");
                    }
                }

                //Check the role if exist permissions
                var result2 = await GetPermissionsInRole(roleId);

                //Get the same role to be updated
                var checkexist = await _context.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
                if (result2.Count() > 0)
                {

                    //Subtract the Existing Permissions from the selected permissions
                    var remainingperms = result2.Except(mappingDTO).PackPermissionsIntoString();

                    if (!string.IsNullOrEmpty(remainingperms))
                    {
                        checkexist.Id = roleId;
                        checkexist.PermissionsInRole = remainingperms;
                    }
                    else
                    {
                        checkexist.Id = roleId;
                        checkexist.PermissionsInRole = string.Empty;
                    }
                    await _context.SaveChangesAsync();
                    return "1";
                }
                else
                {
                    checkexist.Id = roleId;
                    checkexist.PermissionsInRole = mappingDTO.PackPermissionsIntoString();
                    await _context.SaveChangesAsync();
                    return "1";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }

        #endregion

        public Task<string> GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task<BtUser> GenerateToken(string username, string refereshToken = null)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserLoginHistoryViewModel>> GetUserLoginHistoryByMerchant()
        {
            throw new NotImplementedException();
        }

        public ReactivationRequestViewModel GetUserReactivationReq(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReactivationRequestViewModel> GetUsersRactivationRequests()
        {
            throw new NotImplementedException();
        }

        public async Task<string> InAppPasswordReset(PasswordInAppResetDTO inAppResetDTO)
        {
            try
            {
                return string.Empty;
                ////Get the Logged in User
                //var userAuthname = _authUser.Name;

                ////Check if the password meet the Policy
                //var passpolicy = _userManager.Options.Password;

                ////Get Details of Logged in User
                //var getUser = await _userManager.FindByNameAsync(userAuthname);

                ////Check if the new password equates the current password
                //var verifywithoriginal = _userManager.PasswordHasher.VerifyHashedPassword(getUser, getUser?.PasswordHash, inAppResetDTO.password);

                ////Second Level Validation
                //var npass = _userManager.PasswordHasher.VerifyHashedPassword(getUser, getUser?.PasswordHash, inAppResetDTO.currentpassword);
                //if (npass == PasswordVerificationResult.Failed)
                //{
                //    _logger.LogWarning($"Your current password is not valid. Please enter valid password");
                //}

                ////3rd Level Validation
                ////Check the history table for password relatin to the new password
                ////var oldPassword = _context.TGnsysUsersPasswordHist.Include(x => x.Emp).Where(x => x.Emp.GnsysUserid == getUser.Id);
                ////if (oldPassword.Any(x => _userManager.PasswordHasher.VerifyHashedPassword(getUser, x.PwdEncrypt, inAppResetDTO.password) == PasswordVerificationResult.Success))
                ////{
                ////    _logger.LogWarning($"{getUser.UserName} new password already exists in the history of old password. please choose another password combination!");
                ////    await Bus.RaiseEvent(new DomainNotification("Error", $"{getUser.UserName} new password already exists in the history of old password. please choose another password combination!"));
                ////    return await Task.FromResult(false);
                ////}

                ////4th Level Validation
                //var result = AuthenticatedUsernewpasswordPolicyValidator.ValidatePasswordPolicy(inAppResetDTO.currentpassword, passpolicy, userAuthname, verifywithoriginal, getUser.Email);
                //if (result.Count() > 0)
                //{
                //    foreach (var item in result)
                //    {
                //        _logger.LogWarning(item);
                //        //await Bus.RaiseEvent(new DomainNotification(request.MessageType, item));
                //    }
                //    //return await Task.FromResult(false);
                //}

                ////var fetchempid = await _context.TGnsysEmployee.FirstOrDefaultAsync(x => x.GnsysUserid == getUser.Id);
                ////var passcount = (oldPassword.Count() > 0) ? oldPassword.Select(x => x.PwdCount).Max() + 1 : 1;
                ////Before Change save the previos password to history
                //BTUsersPasswordHist hist = new BTUsersPasswordHist
                //{
                //    HistryId = Guid.NewGuid().ToString(),
                //    EmpId = fetchempid.EmpId,
                //    PwdEncrypt = getUser?.PasswordHash,
                //    CreatedBy = getUser?.UserName,
                //    CreatedDate = DateTime.Now,
                //    LastModified = null,
                //    ModifiedBy = null,
                //    PwdCount = passcount
                //};
                //_dbcontext.TGnsysUsersPasswordHist.Add(hist);
                //await _dbcontext.SaveChangesAsync();
                ////Update the Identity Password 

                //var passwordchange = await _userManager.ChangePasswordAsync(getUser, request.CurrentPassword, request.Password);
                //if (passwordchange.Succeeded)
                //{
                //    return await Task.FromResult(true);
                //}
                //else
                //{
                //    await Bus.RaiseEvent(new DomainNotification(request.MessageType, "Sorry! Password could not be change. Try again Later"));
                //    return await Task.FromResult(false);
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Task<string> LogoutUser()
        {
            throw new NotImplementedException();
        }


        public Task OutAppPasswordReset(string username)
        {
            throw new NotImplementedException();
        }

        public Task ResetPassword(PasswordResetOuterDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLoginHistory(BtUserLoginHistory obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserReactivationRequest(string status, string desc, DateTime changedate, string ModifiedBy, int userid, string Id)
        {
            throw new NotImplementedException();
        }
    }
}
