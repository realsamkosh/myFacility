using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using myFacility.Infrastructure;
using myFacility.Models.Domains.Account;
using myFacility.Services;
using myFacility.Utilities.AuthenticationUtility;

namespace myFacility.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                 .AddEnvironmentVariables();
            if (env.IsDevelopment())
            {
                Configuration = builder.Build();
            }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<authDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuthDbConnection")));
            services.AddDbContext<myFacilityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyFacilityDbConnection")));

            services.AddIdentity<BtUser, BtRole>()
                .AddEntityFrameworkStores<authDbContext>()
                .AddDefaultTokenProviders()
                .AddPasswordValidator<UsernameAsPasswordValidator<BtUser>>()
                .AddPasswordValidator<EmailAsPasswordValidator<BtUser>>();
            //.AddGemeniPasswordLinkExpirationTokenProvider();


            services.AddHangfire(x => x
           .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
           .UseSqlServerStorage(Configuration.GetConnectionString("myFacilityDbConnection")));

            services.AddControllersWithViews();

            // .NET Native DI Abstraction
            RegisterServices(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
