using myFacility.Infrastructure;
using myFacility.Models.Domains.Account;
using myFacility.Utilities.Extensions.Permission;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace myFacility.Services.Authorization
{
    // Thanks to https://korzh.com/blogs/net-tricks/aspnet-identity-store-user-data-in-claims
    public class AddPermissionsToUserClaims : UserClaimsPrincipalFactory<BtUser,BtRole>
    {
        private readonly authDbContext _context;

        public AddPermissionsToUserClaims(UserManager<BtUser> userManager, RoleManager<BtRole> roleManager, IOptions<IdentityOptions> optionsAccessor,
            authDbContext context)
            : base(userManager, roleManager, optionsAccessor)
        {
            _context = context;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(BtUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var userId = identity.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var permissionforuser = (await (from roles in _context.Roles
                                            join userroles in _context.UserRoles on roles.Id equals userroles.RoleId
                                            where userroles.UserId == userId
                                            select roles.PermissionsInRole.UnpackPermissionsFromString()).ToListAsync())
                //Because the permissions are packed we have to put these parts of the query after the ToListAsync()
                .SelectMany(x => x).Distinct();

            var result  = permissionforuser.PackPermissionsIntoString();
            var userdata = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            identity.AddClaim(new Claim(PermissionConstants.PackedPermissionClaimType,result));
            identity.AddClaim(new Claim(PermissionConstants.PatientNo, ""));
            identity.AddClaim(new Claim(PermissionConstants.UserCategory, userdata.UserCategory));
            return identity;
        }
    }
}
