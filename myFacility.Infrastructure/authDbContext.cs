using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myFacility.Domain.General.Payment;
using myFacility.Infrastructure.EntityBuilder;
using myFacility.Infrastructure.GeneralEntityMapping;
using myFacility.Model.Domain.General.Messaging;
using myFacility.Models.Domains.Account;
using myFacility.Models.Domains.Location;
using myFacility.Models.Domains.Lookups;
using myFacility.Models.Domains.Messaging;
using myFacility.Models.Domains.Regulator;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Infrastructure
{
    public class authDbContext : IdentityDbContext<BtUser, BtRole, string>
    {
        public authDbContext(DbContextOptions<authDbContext> options) : base(options)
        {

        }
        public virtual DbSet<BtCountry> BtCountry { get; set; }
        public virtual DbSet<BtLga> BtLga { get; set; }
        public virtual DbSet<BtPermissions> BtPermissions { get; set; }
        public virtual DbSet<BtState> BtState { get; set; }
        public virtual DbSet<BtSysConfiguration> BtSysConfiguration { get; set; }
        public virtual DbSet<BtUserLoginHistory> BtUserLoginHistory { get; set; }
        public virtual DbSet<BtUserRole> BtUserRole { get; set; }
        public virtual DbSet<TRegulator> TRegulator { get; set; }
        public virtual DbSet<TEmailLog> TEmailLog { get; set; }
        public virtual DbSet<TSmsLog> TSmsLog { get; set; }
        public virtual DbSet<TEmailToken> TEmailToken { get; set; }
        public virtual DbSet<TEmailtemplate> TEmailtemplate { get; set; }
        public virtual DbSet<TSmsToken> TSmsToken { get; set; }
        public virtual DbSet<TSmstemplate> TSmstemplate { get; set; }
      
        public virtual DbSet<TTransactionLog> TTransactionLog { get; set; }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Payment
            new PaymentEntityMapping(builder.Entity<TTransactionLog>());

            //Regualtor
            new RegulatorEntityMapping(builder.Entity<TRegulator>());

            // Lookups
            new BtSysConfigurationEntityMapping(builder.Entity<BtSysConfiguration>());

            //Location
            new TCountryEntityMapping(builder.Entity<BtCountry>());
            new TStateEntityMapping(builder.Entity<BtState>());
            new TLgaEntityMapping(builder.Entity<BtLga>());

            //Users (Applicant, Regulator, Institutions)
            new BtUserLoginHistoryEntityMapping(builder.Entity<BtUserLoginHistory>());

            //Messaging
            new EmailTemplateEntityMapping(builder.Entity<TEmailtemplate>());
            new TEmailLogEntityMapping(builder.Entity<TEmailLog>());
            new TEmailTokenEntityMapping(builder.Entity<TEmailToken>());
            new TSmsTokenEntityMapping(builder.Entity<TSmsToken>());
            new TSmsLogEntityMapping(builder.Entity<TSmsLog>());
            new TSmsTemplateEntityMapping(builder.Entity<TSmstemplate>());

            ///<summary>
            /// User Mapping to Database
            ///</summary>
            #region User
            new BtUsersEntityMapping(builder.Entity<BtUser>());
            new UserPermissionMapping(builder.Entity<BtPermissions>());
            new TUserRoleMapping(builder.Entity<BtUserRole>());
            new TRolesEntityMapping(builder.Entity<BtRole>());
            //new PermissionsInRoleQueryMapping(builder.Entity<myFacilityPermissionsInRole>());
            //new TGenesysRolePermissionEntityMapping(builder.Entity<BtRolePermission>());
            #endregion

            ///<summary>
            /// Identity Remapping
            /// </summary>
            #region Identity Remapping
            builder.Entity<IdentityRoleClaim<string>>().ToTable("bt_role_claim");
            builder.Entity<IdentityUserClaim<string>>().ToTable("bt_user_claim");
            builder.Entity<IdentityUserToken<string>>().ToTable("bt_user_token");
            builder.Entity<IdentityUserRole<string>>().ToTable("bt_user_role");
            builder.Entity<IdentityUserLogin<string>>().ToTable("bt_user_login");
            #endregion
        }
    }
}
