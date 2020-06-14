using myFacility.Utilities.Extensions.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFacility.Services.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly ILogger<PermissionAuthorizationHandler> _logger;

        public PermissionAuthorizationHandler(ILogger<PermissionAuthorizationHandler> logger)
        {
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            var permissionsClaim = context.User.Claims.SingleOrDefault(c => c.Type == PermissionConstants.PackedPermissionClaimType);
            // If user does not have the scope claim, get out of here
            if (permissionsClaim == null)
                return Task.CompletedTask;

            if (permissionsClaim.Value.ThisPermissionIsAllowed(requirement.PermissionName))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
