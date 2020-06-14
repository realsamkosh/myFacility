using myFacility.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace myFacility.Utilities.Extensions.Permission
{
    public static class PermissionChecker
    {
        /// <summary>
        /// This is used by the policy provider to check the permission name string
        /// </summary>
        /// <param name="packedPermissions"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public static bool ThisPermissionIsAllowed(this string packedPermissions, string permissionName)
        {
            var usersPermissions = packedPermissions.UnpackPermissionsFromString().ToArray();

            if (!Enum.TryParse(permissionName, true, out myFacilityPermissions permissionToCheck))
                throw new InvalidEnumArgumentException($"{permissionName} could not be converted to a {nameof(myFacilityPermissions)}.");

            return usersPermissions.UserHasThisPermission(permissionToCheck);
        }

        /// <summary>
        /// This is the main checker of whether a user permissions allows them to access something with the given permission
        /// </summary>
        /// <param name="usersPermissions"></param>
        /// <param name="permissionToCheck"></param>
        /// <returns></returns>
        public static bool UserHasThisPermission(this myFacilityPermissions[] usersPermissions, myFacilityPermissions permissionToCheck)
        {
            return usersPermissions.Contains(permissionToCheck) || usersPermissions.Contains(myFacilityPermissions.AccessAll);
        }
    }
}
