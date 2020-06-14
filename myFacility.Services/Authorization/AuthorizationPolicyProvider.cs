using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace myFacility.Services.Authorization
{
    public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;

        public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            _options = options.Value;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            //Unit tested shows this is quicker (and safer - see link to issue above) than the original version
            return await base.GetPolicyAsync(policyName) ?? new AuthorizationPolicyBuilder()
                       .AddRequirements(new PermissionAuthorizationRequirement(policyName))
                       .Build();
        }
    }
}
