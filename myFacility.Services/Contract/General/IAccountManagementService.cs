using myFacility.Models.DataObjects.Account;
using myFacility.Models.Domains.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myFacility.Services.Contract
{
    public interface IAccountManagementService
    {
        #region Role Interfaces
        Task<string> CreateRoleAsync(RoleDTO model);
        string GetRoleIDConverter(string id);
        Task<IEnumerable<RoleViewModel>> GetAllRolesAsync();
        Task<RoleCollectionViewModel> GetAssignedAndUnAssignedUserToRole(string Id);
        Task DeleteRoleAsync(string id);
        Task<string> UpdateRoleAsync(RoleDTO obj, string id);
        Task<RoleViewModel> GetRole(string id);
        Task<string> AssignUsersToRole(string roleId, string[] userIds);
        Task<string> RemoveUsersFromRole(string roleId, string[] userIds);
        //Task<string> MoveUsersToNewRoleAsync(string NewRoleId, string[] userIds);
        #endregion

        #region Authentication
        /// <summary>
        /// Interface for Generating Token
        /// </summary>
        /// <param name="username"> Current Logged in User</param>
        /// <param name="refereshToken">Used to revalidate user Access Token</param>
        /// <returns></returns>
        Task<BtUser> GenerateToken(string username, string refereshToken = null);
        Task<SignInResult> AuthenticateUser(string username, string password);
        Task<string> GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<string> LogoutUser();
        Task<string> ConfirmEmail(string userid, string code);
        Task<string> Confirmations(string username);
        #endregion

        #region Permission Interfaces
        Task<IEnumerable<PermissionViewModel>> GetPermissions();
        Task<IEnumerable<PermissionViewModel>> GetUserPermissions();
        Task<PermissionViewModel> GetPermission(string permissionId);
        Task<PermissionCollectionViewModel> GetMerchantPermissionsByRole(string id);
        Task<string> AddPermissionsToRole(string roleId, string[] permissionId);
        Task<string> RemovePermissionsFromRole(string roleId, string[] permissionId);
        Task<string> CalcPermissionsForUserAsync(string userId);
        #endregion

        #region Password Management Interface
        Task<string> InAppPasswordReset(PasswordInAppResetDTO inAppResetDTO);
        Task OutAppPasswordReset(string username);
        Task ResetPassword(PasswordResetOuterDTO obj);
        #endregion

        #region Login History
        Task CreateLoginHistory(BtUserLoginHistory obj);
        Task CreateLastLoginDate(string userid);
        Task<IEnumerable<UserLoginHistoryViewModel>> GetUserLoginHistoryByMerchant();
        Task UpdateLoginHistory(BtUserLoginHistory obj);
        #endregion

        #region User Reactivation Management
        Task<bool> CreateReactivationRequest(ReactivationRequestDTO obj);
        IEnumerable<ReactivationRequestViewModel> GetUsersRactivationRequests();
        ReactivationRequestViewModel GetUserReactivationReq(string id);
        Task<bool> UpdateUserReactivationRequest(string status, string desc, DateTime changedate, string ModifiedBy, int userid, string Id);
        #endregion
    }
}
