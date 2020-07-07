using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using myFacility.Models.Domains.Account;
using myFacility.Services.Authorization;
using myFacility.Services.Contract;
using myFacility.Services.Handler;
using myFacility.Utilities.AuthenticationUtility.AuthUser;

namespace myFacility.Services
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped(typeof(TenantIdentification));
            // ASP.NET Authorization Polices
            //Register the Permission policy handlers
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddScoped<IUserClaimsPrincipalFactory<BtUser>, AddPermissionsToUserClaims>();
            // Services
            services.AddTransient<ILocationManagementService, LocationManagementService>();
            services.AddTransient<ISeedingManagementService, SeedingManagementService>();
            services.AddTransient<ICommonServices, CommonServices>();
            services.AddTransient<IAccountManagementService, AccountManagementService>();
            services.AddTransient<IMailManagementService, MailManagementService>();
            services.AddTransient<IMessageManagementService, MessageManagementService>();
            services.AddTransient<IUserManagementServices, UserManagementServices>();

            services.AddTransient<StatisticsService>();


            // Domain.Core - Identity
            services.AddScoped<IAuthUser, AuthUser>();

        }
    }
}
