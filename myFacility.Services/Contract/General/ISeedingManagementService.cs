using myFacility.Models.Domains.Account;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace myFacility.Services.Contract
{
    public interface ISeedingManagementService
    {
        Task AutoSeedGlobalAdmin();
        /// <summary>
        /// This Interface is used to seed Country, State and Local Goverment Data into the database
        /// </summary>
        void AutoSeedCountries();
        /// <summary>
        /// Seed Default Roles for the System
        /// </summary>
        void AutoSeedApplicationRoles(RoleManager<BtRole> roleManager);


        #region Email Template
        void AutoSeedEmailTemplates();
        void AutoSeedSmsTemplates();
        #endregion

        
        void AutoSeedEmailToken();
        void AutoSeedSmsToken();
        void AutoSeedSystemConfigurations();
    }
}
