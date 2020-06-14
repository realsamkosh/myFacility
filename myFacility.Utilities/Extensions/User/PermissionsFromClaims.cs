using myFacility.Utilities.Enums;
using myFacility.Utilities.Extensions.Permission;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace myFacility.Utilities.Extensions.User
{
    public static class PermissionsFromClaims
    {
        /// <summary>
        /// This gets the permissions for the currently logged in user (or null if no claim)
        /// </summary>
        /// <param name="usersClaims"></param>
        /// <returns></returns>
        public static IEnumerable<myFacilityPermissions> UserPermissionsFromClaims(this IEnumerable<Claim> usersClaims)
        {
            var permissionsClaim =
                usersClaims?.SingleOrDefault(c => c.Type == PermissionConstants.PackedPermissionClaimType);
            return permissionsClaim?.Value.UnpackPermissionsFromString();
        }
    }
}
