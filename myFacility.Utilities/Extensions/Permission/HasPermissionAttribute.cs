using myFacility.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using System;

namespace myFacility.Utilities.Extensions.Permission
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(myFacilityPermissions permission) : base(permission.ToString())
        { }
    }
}
