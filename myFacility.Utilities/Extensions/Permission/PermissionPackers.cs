using myFacility.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace myFacility.Utilities.Extensions.Permission
{
    public static class PermissionPackers
    {
        public static string PackPermissionsIntoString(this IEnumerable<myFacilityPermissions> permissions)
        {
            return permissions.Aggregate("", (s, permission) => s + (char)permission);
        }

        public static IEnumerable<myFacilityPermissions> UnpackPermissionsFromString(this string packedPermissions)
        {
            if (packedPermissions == null)
                throw new ArgumentNullException(nameof(packedPermissions));
            foreach (var character in packedPermissions)
            {
                yield return ((myFacilityPermissions)character);
            }
        }

        public static myFacilityPermissions? FindPermissionViaName(this string permissionName)
        {
            return Enum.TryParse(permissionName, out myFacilityPermissions permission)
                ? (myFacilityPermissions?)permission
                : null;
        }
    }
}
