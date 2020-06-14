using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using myFacility.Infrastructure;
using myFacility.Models.Domains.Account;
using myFacility.Models.Domains.Location;
using myFacility.Models.Domains.Lookups;
using myFacility.Models.Domains.Messaging;
using myFacility.Services.Contract;
using myFacility.Utilities.Enums;
using myFacility.Utilities.Extensions.Permission;

namespace myFacility.Services.Handler
{
    public class SeedingManagementService : ISeedingManagementService
    {
        private readonly authDbContext _context;
        private readonly RoleManager<BtRole> _roleManager;
        //private readonly IHostingEnvironment _hostenv;
        private readonly UserManager<BtUser> _userManager;
        private readonly ILogger<SeedingManagementService> _logger;

        public SeedingManagementService(ILogger<SeedingManagementService> logger, authDbContext context, RoleManager<BtRole> roleManager,
             //IHostingEnvironment hostenv,
             UserManager<BtUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            //_hostenv = hostenv;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task AutoSeedGlobalAdmin()
        {
            try
            {
                //Create Global Admin user for the application
                var user = await _userManager.FindByNameAsync("admin");
                if (user == null)
                {
                    var testUser = new BtUser
                    {
                        UserName = "admin",
                        Email = "admin@myFacility.com",
                        EmailConfirmed = true,
                        CreatedBy = "System",
                        CreatedDate = DateTime.Now,
                        NormalizedUserName = "admin@myFacility.com".ToUpper(),
                        TwoFactorEnabled = false,
                        ForcePwdChange = false,
                        UserCategory = "A",
                        FirstName = "Admin",
                        LastName = "Global",
                        MiddleName = "",
                        PwdExpiryDate = DateTime.Now.AddYears(1000),
                        LockoutEnabled = false,
                        PhoneNumber = "NIL",
                        IsPrivacyPolicyAgreed = false
                    };
                    await _userManager.CreateAsync(testUser, "P@ssw0rd");

                    //Add role for user
                    var adminRole = await _roleManager.FindByNameAsync("GlobalAdmin");
                    if (adminRole == null)
                    {
                        var fetchallpermissions = Enum.GetValues(typeof(myFacilityPermissions)).Cast<myFacilityPermissions>()
                           .ToList();
                        var NewadminRole = new BtRole
                        {
                            Name = "GlobalAdmin",
                            RoleDesc = "This is the Global Administrative Role",
                            ModifiedBy = "Admin",
                            LastModified = DateTime.Now,
                            CreatedBy = "Admin",
                            IsSuperUser = true,
                            CreatedDate = DateTime.Now,
                            IsActive = true,
                            PermissionsInRole = fetchallpermissions.Where(x => x == myFacilityPermissions.AccessAll).PackPermissionsIntoString()
                        };
                        await _roleManager.CreateAsync(NewadminRole);

                        //Add to user role
                        //BtUserRole btUserRole = new BtUserRole
                        //{
                        //    RoleId = NewadminRole.Id,
                        //    UserId = testUser.Id,
                        //    //ModifiedBy = "System",
                        //    //LastModified = DateTime.Now
                        //};

                        await _userManager.AddToRoleAsync(testUser, NewadminRole.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void AutoSeedApplicationRoles(RoleManager<BtRole> roleManager)
        {
            throw new NotImplementedException();
        }


        public void AutoSeedCountries()
        {
            if (_context.BtCountry.Any())
                return; //DB has been seeded with country data       
            var country = new BtCountry[]
            {
                new BtCountry { CountryCode = "AD", Name="ANDORRA", NationalCurrency="EUR",Nationality="Andorran", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AE", Name="UNITED ARAB EMIRATES", NationalCurrency="AED", Nationality="Emirati", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AF", Name="AFGHANISTAN", NationalCurrency="AFN", Nationality="Afgans", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AG", Name="ANTIGUA AND BARBUDA",  NationalCurrency="XCD", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AI", Name="ANGUILLA", NationalCurrency="XCD", Nationality="Anguillan", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AL", Name="ALBANIA", NationalCurrency="ALL", Nationality="Albanian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AM", Name="ARMENIA",NationalCurrency="AMD", Nationality="Armenians", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AN", Name="NETHERLANDS ANTILLES",NationalCurrency="ANG", Nationality="Dutch", CreatedBy="System", CreatedDate=DateTime.Now, ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AO", Name="ANGOLA",NationalCurrency="AOA", CreatedBy="System", Nationality="Angolans",CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AQ", Name="ANTARCTICA",NationalCurrency="N/A", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AR", Name="ARGENTINA",NationalCurrency="ARS", Nationality="Argentine", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AS", Name="AMERICAN SAMOA",NationalCurrency="USD", Nationality="American Samoans",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AT", Name="AUSTRIA",NationalCurrency="EUR", Nationality="Austrian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AU", Name="AUSTRALIA",NationalCurrency="AUD", Nationality="Australian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AW", Name="ARUBA",NationalCurrency="AWG", Nationality="Dutch Caribbean", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AX", Name="ÅLAND ISLANDS",NationalCurrency="EUR",Nationality="Finnish",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "AZ", Name="AZERBAIJAN",NationalCurrency="AZN", Nationality="Azerbaijani",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BA", Name="BOSNIA AND HERZEGOVINA",NationalCurrency="BAM", Nationality="Bosnian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BB", Name="BARBADOS",NationalCurrency="BBD",Nationality="Barbadians",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BD", Name="BANGLADESH",NationalCurrency="BDT", Nationality="Bangladeshi", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BE", Name="BELGIUM",NationalCurrency="EUR", Nationality="Belgians",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BF", Name="BURKINA FASO",NationalCurrency="XOF", Nationality="Burkinabe",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BG", Name="BULGARIA",NationalCurrency="BGN", Nationality="Bulgarian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BH", Name="BAHRAIN",NationalCurrency="BHD", Nationality="Bahraini", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BI", Name="BURUNDI",NationalCurrency="BIF",Nationality="Burundian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BJ", Name="BENIN" ,NationalCurrency="XOF",Nationality="Beninese",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BL", Name="SAINT BARTHÉLEMY",NationalCurrency="EUR", Nationality="SAINT BARTHÉLEMOIS", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BM", Name="BERMUDA",NationalCurrency="BMD", Nationality="Bermudian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BN", Name="BRUNEI DARUSSALAM",NationalCurrency="BND", Nationality="Bruneian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BO", Name="BOLIVIA",NationalCurrency="BOB", Nationality="Bolivian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BQ", Name="BONAIRE, SAINT EUSTATIUS AND SABA",NationalCurrency="USD", Nationality="Not Set",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BR", Name="BRAZIL",NationalCurrency="BRL", Nationality="Bazillian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BS", Name="BAHAMAS",NationalCurrency="BSD", Nationality="Bahamian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BT", Name="BHUTAN",NationalCurrency="BTN", Nationality="Bhutanese", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BV", Name="BOUVET ISLAND",NationalCurrency="NOK", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BW", Name="BOTSWANA",NationalCurrency="BWP", Nationality="Batswana", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BY", Name="BELARUS",NationalCurrency="BYR", Nationality="Belarussian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "BZ", Name="BELIZE",NationalCurrency="BZD", Nationality="Belizean",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CA", Name="CANADA",NationalCurrency="CAD", Nationality="Canadaian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CC", Name="COCOS (KEELING) ISLANDS",NationalCurrency="AUD", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CD", Name="CONGO, THE DEMOCRATIC REPUBLIC OF THE",NationalCurrency="CDF", Nationality="Congolese",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CF", Name="CENTRAL AFRICAN REPUBLIC",NationalCurrency="XAF", Nationality="Central African",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CG", Name="CONGO",NationalCurrency="XAF", Nationality="Congolese",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CH", Name="SWITZERLAND",NationalCurrency="CHF", Nationality="Swiss", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CI", Name="COTE D'IVOIRE",NationalCurrency="XOF", Nationality="Ivorian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CK", Name="COOK ISLANDS",NationalCurrency="NZD", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CL", Name="CHILE",NationalCurrency="CLP", Nationality="Chilean",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CM", Name="CAMEROON",NationalCurrency="XAF", Nationality="Cameroonian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CN", Name="CHINA",NationalCurrency="CNY", Nationality="Chinese",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CO", Name="COLOMBIA",NationalCurrency="COP", Nationality="Colombian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CR", Name="COSTA RICA",NationalCurrency="CRC", Nationality="Costa Rican", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CU", Name="CUBA",NationalCurrency="CUP",  Nationality="Cuban", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CV", Name="CAPE VERDE",NationalCurrency="CVE", Nationality="Cape Verdean", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CW", Name="CURACAO",NationalCurrency="ANG", Nationality="Dutch", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CX", Name="CHRISTMAS ISLAND",NationalCurrency="AUD", Nationality="Christmas Islander", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CY", Name="CYPRUS",NationalCurrency="EUR", Nationality="Cypriot", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "CZ", Name="CZECH REPUBLIC",NationalCurrency="CZK", Nationality="Czech", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "DE", Name="GERMANY",NationalCurrency="EUR", Nationality="German", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "DJ", Name="DJIBOUTI",NationalCurrency="DJF", Nationality="Djibouti", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "DK", Name="DENMARK",NationalCurrency="DKK", Nationality="Dane", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "DM", Name="DOMINICA",NationalCurrency="XCD", Nationality="Dominican", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "DO", Name="DOMINICAN REPUBLIC",NationalCurrency="DOP", Nationality="Dominican", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "DZ", Name="ALGERIA",NationalCurrency="DZD", Nationality="Algerian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "EC", Name="ECUADOR",NationalCurrency="USD", Nationality="Ecuadorian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "EE", Name="ESTONIA",NationalCurrency="EUR", Nationality="Estonian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "EG", Name="EGYPT",NationalCurrency="EGP", Nationality="Egyptian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "EH", Name="WESTERN SAHARA",NationalCurrency="MAD", Nationality="Saharawi", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "ER", Name="ERITREA",NationalCurrency="ERN", Nationality="Eritean", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "ES", Name="SPAIN",NationalCurrency="EUR", Nationality="Spaniard", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "ET", Name="ETHIOPIA",NationalCurrency="ETB", Nationality="Ethiopian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "FI", Name="FINLAND",NationalCurrency="EUR",Nationality="Finn", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "FJ", Name="FIJI",NationalCurrency="FJD", Nationality="Fijian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "FK", Name="FALKLAND ISLANDS (MALVINAS)",NationalCurrency="FKP", Nationality="Falkland Islander", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "FM", Name="MICRONESIA, FEDERATED STATES OF",NationalCurrency="USD", Nationality="Micronesian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "FO", Name="FAROE ISLANDS",NationalCurrency="DKK", CreatedBy="System", Nationality="Faroese",  CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "FR", Name="FRANCE",NationalCurrency="EUR", Nationality="FrenchPerson", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "FX", Name="FRANCE, METROPOLITAN",NationalCurrency="EUR", Nationality="FrenchPerson", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GA", Name="GABON",NationalCurrency="XAF", Nationality="Gabonese", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GB", Name="UNITED KINGDOM",NationalCurrency="GBP", Nationality="Brit", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GD", Name="GRENADA",NationalCurrency="XCD", Nationality="Grenadian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GE", Name="GEORGIA",NationalCurrency="GEL", Nationality="Georgian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GF", Name="FRENCH GUIANA",NationalCurrency="EUR", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GG", Name="GUERNSEY",NationalCurrency="GBP", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GH", Name="GHANA",NationalCurrency="GHS", Nationality="Ghanaian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GI", Name="GIBRALTAR",NationalCurrency="GIP", Nationality="Gibraltarian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GL", Name="GREENLAND",NationalCurrency="DKK", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GM", Name="GAMBIA",NationalCurrency="GMD", Nationality="Gambian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GN", Name="GUINEA",NationalCurrency="GNF", Nationality="Guinean", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GP", Name="GUADELOUPE",NationalCurrency="EUR", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GQ", Name="EQUATORIAL GUINEA",NationalCurrency="XAF", Nationality="Equatoguinean", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GR", Name="GREECE",NationalCurrency="EUR", Nationality="Greek", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GS", Name="SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS",NationalCurrency="GBP", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GT", Name="GUATEMALA",NationalCurrency="GTQ", Nationality="Guatemalan", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GU", Name="GUAM",NationalCurrency="USD", Nationality="Guamanian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GW", Name="GUINEA-BISSAU",NationalCurrency="XOF", Nationality="Bissau Guinean", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "GY", Name="GUYANA",NationalCurrency="GYD", Nationality="Guyanese", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "HK", Name="HONG KONG",NationalCurrency="HKD", Nationality="Hong konger", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "HM", Name="HEARD ISLAND AND MCDONALD ISLANDS",NationalCurrency="AUD", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "HN", Name="HONDURAS",NationalCurrency="HNL", Nationality="Hondurian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "HR", Name="CROATIA (local name: Hrvatska)",NationalCurrency="HRK", Nationality="Croat", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "HT", Name="HAITI",NationalCurrency="HTG ", Nationality="Haitian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "HU", Name="HUNGARY",NationalCurrency="HUF", Nationality="Hungarian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "ID", Name="INDONESIA",NationalCurrency="IDR", Nationality="Indonesian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "IE", Name="IRELAND",NationalCurrency="EUR", Nationality="IrishPerson", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "IL", Name="ISRAEL",NationalCurrency="ILS", Nationality="Isreali", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "IM", Name="ISLE OF MAN",NationalCurrency="GBP",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "IN", Name="INDIA",NationalCurrency="INR", Nationality="Indian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "IO", Name="BRITISH INDIAN OCEAN TERRITORY",NationalCurrency="USD", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "IQ", Name="IRAQ",NationalCurrency="IQD", Nationality="Iraqi", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "IR", Name="IRAN, ISLAMIC REPUBLIC OF",NationalCurrency="IRR", Nationality="Iranian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "IS", Name="ICELAND",NationalCurrency="ISK", Nationality="Icelandic", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "IT", Name="ITALY",NationalCurrency="EUR", Nationality="Italian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "JE", Name="JERSEY",NationalCurrency="GBP", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "JM", Name="JAMAICA",NationalCurrency="JMD", Nationality="Jamaican", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "JO", Name="JORDAN	",NationalCurrency="JOD", Nationality="Jordanian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "JP", Name="JAPAN",NationalCurrency="JPY", Nationality="Japanese", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "KE", Name="KENYA",NationalCurrency="KES", Nationality="Kenyan", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "KG", Name="KYRGYZSTAN",NationalCurrency="KGS", Nationality="Kyrgyz", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "KH", Name="CAMBODIA",NationalCurrency="KHR", Nationality="Cambodian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "KI", Name="KIRIBATI",NationalCurrency="AUD", Nationality="Kiribatian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "KM", Name="COMOROS",NationalCurrency="KMF", Nationality="Comorian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "KN", Name="SAINT KITTS AND NEVIS",NationalCurrency="XCD", Nationality="Kettitian and Nevisian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "KP", Name="KOREA, DEMOCRATIC PEOPLE'S REPUBLIC OF",NationalCurrency="KPW", Nationality="Korean",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "KR", Name="KOREA, REPUBLIC OF",NationalCurrency="KRW", CreatedBy="System", Nationality="Korean",  CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "KW", Name="KUWAIT",NationalCurrency="KWD", Nationality="Kuwaiti",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "KY", Name="CAYMAN ISLANDS",NationalCurrency="KYD", Nationality="Caymanians",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "KZ", Name="KAZAKHSTAN",NationalCurrency="KZT", Nationality="Kazakhs",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "LA", Name="LAO PEOPLE'S DEMOCRATIC REPUBLIC",NationalCurrency="LAK", Nationality="Laotian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "LB", Name="LEBANON",NationalCurrency="LBP", Nationality="Lebanese",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "LC", Name="SAINT LUCIA",NationalCurrency="XCD", Nationality="Saint Lucian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "LI", Name="LIECHTENSTEIN",NationalCurrency="CHF",  Nationality="Liechtensteiner",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "LK", Name="SRI LANKA",NationalCurrency="LKR", Nationality="Sri Lankan", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "LR", Name="LIBERIA",NationalCurrency="LRD", Nationality="Liberian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "LS", Name="LESOTHO",NationalCurrency="ZAR", Nationality="Basotho",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "LT", Name="LITHUANIA",NationalCurrency="EUR", Nationality="Lithuanian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "LU", Name="LUXEMBOURG",NationalCurrency="EUR", Nationality="Luxembourger", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "LV", Name="LATVIA",NationalCurrency="EUR", Nationality="Latvian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "LY", Name="LIBYAN ARAB JAMAHIRIYA",NationalCurrency="LYD", Nationality="Libyan", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MA", Name="MOROCCO",NationalCurrency="MAD", Nationality="Moroccan", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MC", Name="MONACO",NationalCurrency="EUR", Nationality="Monegasque", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MD", Name="MOLDOVA",NationalCurrency="MDL", Nationality="Moldovan", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "ME", Name="MONTENEGRO",NationalCurrency="EUR", Nationality="Montenegrin", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MF", Name="SAINT MARTIN (FRENCH PART)",NationalCurrency="EUR", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MG", Name="MADAGASCAR",NationalCurrency="MGA", Nationality="Malagasy", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MH", Name="MARSHALL ISLANDS",NationalCurrency="USD", Nationality="Marshallese", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MK", Name="MACEDONIA, THE FORMER YUGOSLAV REP. OF",NationalCurrency="MKD", Nationality="Macedonians", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "ML", Name="MALI",NationalCurrency="XOF", Nationality="Malian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MM", Name="MYANMAR",NationalCurrency="MMK", Nationality="Burmese", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MN", Name="MONGOLIA",NationalCurrency="MNT", Nationality="Mongolian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MO", Name="MACAO",NationalCurrency="MOP", Nationality="Macanese", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MP", Name="NORTHERN MARIANA ISLANDS",NationalCurrency="USD",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MQ", Name="MARTINIQUE",NationalCurrency="EUR",Nationality="Martiniquais", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MR", Name="MAURITANIA",NationalCurrency="MRO", Nationality="Mauritanian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MS", Name="MONTSERRAT",NationalCurrency="XCD", Nationality="Montserratian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MT", Name="MALTA",NationalCurrency="EUR", Nationality="Maltese", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MU", Name="MAURITIUS",NationalCurrency="MUR", Nationality="Mauritian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MV", Name="MALDIVES",NationalCurrency="MVR", Nationality="Maldivian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MW", Name="MALAWI",NationalCurrency="MWK", Nationality="Malawian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MX", Name="MEXICO",NationalCurrency="MXN", Nationality="Mexican", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MY", Name="MALAYSIA",NationalCurrency="MYR", Nationality="Malasian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "MZ", Name="MOZAMBIQUE",NationalCurrency="MZN", Nationality="Mozambican",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "NA", Name="NAMIBIA",NationalCurrency="ZAR", Nationality="Namibian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "NC", Name="NEW CALEDONIA",NationalCurrency="XPF", Nationality="New Caledonian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "NE", Name="NIGER",NationalCurrency="XOF", Nationality="Nigerien",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "NF", Name="NORFOLK ISLAND",NationalCurrency="AUD", Nationality="Norfolk Islander",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "NG", Name="NIGERIA",NationalCurrency="NGN", Nationality="Nigerian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "NI", Name="NICARAGUA",NationalCurrency="NIO", Nationality="Nicaraguan",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "NL", Name="NETHERLANDS",NationalCurrency="EUR", Nationality="Dutch Person",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "NO", Name="NORWAY",NationalCurrency="NOK", Nationality="Norwegian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "NP", Name="NEPAL",NationalCurrency="NPR", Nationality="Nepalese",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "NR", Name="NAURU",NationalCurrency="AUD", Nationality="Nauruan",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "NU", Name="NIUE",NationalCurrency="NZD", Nationality="Niueans",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "NZ", Name="NEW ZEALAND",NationalCurrency="NZD", Nationality="New Zealander",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "OM", Name="OMAN",NationalCurrency="OMR", Nationality="Omani",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PA", Name="PANAMA",NationalCurrency="PAB", Nationality="Panamanian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PE", Name="PERU",NationalCurrency="PEN", Nationality="Peruvian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PF", Name="FRENCH POLYNESIA",NationalCurrency="XPF", Nationality="Polynesian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PG", Name="PAPUA NEW GUINEA",NationalCurrency="PGK",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PH", Name="PHILIPPINES",NationalCurrency="PHP", Nationality="Filipino",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PK", Name="PAKISTAN",NationalCurrency="PKR", Nationality="Pakistani",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PL", Name="POLAND",NationalCurrency="PLN",  Nationality="Pole", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PM", Name="ST. PIERRE AND MIQUELON",NationalCurrency="EUR", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PN", Name="PITCAIRN",NationalCurrency="NZD",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PR", Name="PUERTO RICO",NationalCurrency="USD", Nationality="Puerto Rican",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PS", Name="PALESTINIAN TERRITORY, OCCUPIED",NationalCurrency="ILS", Nationality="Palestinian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PT", Name="PORTUGAL",NationalCurrency="EUR", Nationality="Portugese",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PW", Name="PALAU",NationalCurrency="USD",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "PY", Name="PARAGUAY",NationalCurrency="PYG", Nationality="Paraguayan",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "QA", Name="QATAR",NationalCurrency="QAR", Nationality="Qatari",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "RE", Name="REUNION",NationalCurrency="EUR",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "RO", Name="ROMANIA",NationalCurrency="RON", Nationality="Romanian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "RS", Name="SERBIA",NationalCurrency="RSD", Nationality="Serbian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "RU", Name="RUSSIAN FEDERATION",NationalCurrency="RUB", Nationality="Russian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "RW", Name="RWANDA",NationalCurrency="RWF", Nationality="Not Set",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SA", Name="SAUDI ARABIA",NationalCurrency="SAR", Nationality="Saudi Arabian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SB", Name="SOLOMON ISLANDS",NationalCurrency="SBD", Nationality="Not Set",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SC", Name="SEYCHELLES",NationalCurrency="SCR", Nationality="Not Set",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SD", Name="SUDAN",NationalCurrency="SDG", Nationality="Sudanese",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SE", Name="SWEDEN",NationalCurrency="SEK", Nationality="Swede",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SG", Name="SINGAPORE",NationalCurrency="SGD", Nationality="Singaporean",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SH", Name="ST. HELENA, ASCENSION AND TRISTAN DA CUNHA",NationalCurrency="SHP",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SI", Name="SLOVENIA",NationalCurrency="EUR",  Nationality="slovanian",  CreatedBy="System",  CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SJ", Name="SVALBARD AND JAN MAYEN",NationalCurrency="NOK", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SK", Name="SLOVAKIA (Slovak Republic)",NationalCurrency="EUR", Nationality="Slovak",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SL", Name="SIERRA LEONE",NationalCurrency="SLL", Nationality="Sierra leonean",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SM", Name="SAN MARINO",NationalCurrency="EUR",  Nationality="Sammarinese", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SN", Name="SENEGAL",NationalCurrency="XOF", Nationality="Senegalese",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SO", Name="SOMALIA",NationalCurrency="SOS", Nationality="Somali",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SR", Name="SURINAME",NationalCurrency="SRD", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SS", Name="SOUTH SUDAN",NationalCurrency="SSP", Nationality="Sudanese",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "ST", Name="SAO TOME AND PRINCIPE",NationalCurrency="STD", Nationality="Sao Tomean",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SV", Name="EL SALVADOR",NationalCurrency="USD", Nationality="Salvadorean",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SX", Name="SINT MAARTEN (DUTCH PART)",NationalCurrency="ANG",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SY", Name="SYRIAN ARAB REPUBLIC",NationalCurrency="SYP", Nationality="Syrian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "SZ", Name="SWAZILAND",NationalCurrency="SZL", Nationality="Swazi",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TC", Name="TURKS AND CAICOS ISLANDS",NationalCurrency="USD", Nationality="Belongers", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TD", Name="CHAD",NationalCurrency="XAF", Nationality="Chadian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TF", Name="FRENCH SOUTHERN TERRITORIES",NationalCurrency="EUR", Nationality="Not Set", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TG", Name="TOGO",NationalCurrency="XOF", CreatedBy="System", Nationality="Togolese", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TH", Name="THAILAND",NationalCurrency="THB", Nationality="Thai", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TJ", Name="TAJIKISTAN",NationalCurrency="TJS", Nationality="Tajikistani", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TK", Name="TOKELAU",NationalCurrency="NZD", Nationality="Tokelau", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TL", Name="TIMOR-LESTE",NationalCurrency="USD", Nationality="portuguese", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TM", Name="TURKMENISTAN",NationalCurrency="TMT", Nationality="Turkmen", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TN", Name="TUNISIA",NationalCurrency="TND", Nationality="Tunisian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TO", Name="TONGA",NationalCurrency="TOP", Nationality="Tongan", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TR", Name="TURKEY",NationalCurrency="TRY", Nationality="Turk", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TT", Name="TRINIDAD AND TOBAGO",NationalCurrency="TTD",  Nationality="Trinidian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TV", Name="TUVALU",NationalCurrency="AUD", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TW", Name="TAIWAN, PROVINCE OF CHINA",NationalCurrency="TWD", Nationality="Taiwanese", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "TZ", Name="TANZANIA, UNITED REPUBLIC",NationalCurrency="TZS", Nationality="Tanzanian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "UA", Name="UKRAINE",NationalCurrency="UAH", Nationality="Ukranian",  CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "UG", Name="UGANDA",NationalCurrency="UGX", Nationality="Ugandan", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "UM", Name="UNITED STATES MINOR OUTLYING ISLANDS",NationalCurrency="USD", Nationality="American", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "US", Name="UNITED STATES",NationalCurrency="USD", Nationality="American", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "UY", Name="URUGUAY",NationalCurrency="UYU", Nationality="Uruguayan", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "UZ", Name="UZBEKISTAN",NationalCurrency="UZS", Nationality="Uzbek", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "VA", Name="HOLY SEE (VATICAN CITY STATE)",NationalCurrency="EUR", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "VC", Name="SAINT VINCENT AND THE GRENADINES",NationalCurrency="XCD", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "VE", Name="VENEZUELA, BOLIVARIAN REPUBLIC",NationalCurrency="VEF", Nationality="Venezualan", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "VG", Name="VIRGIN ISLANDS (BRITISH)",NationalCurrency="USD", Nationality="Virgin islander", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "VI", Name="VIRGIN ISLANDS (U.S.)",NationalCurrency="USD", Nationality="Virgin islander", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "VN", Name="VIETNAM",NationalCurrency="VND", Nationality="Vietnamese", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "VU", Name="VANUATU",NationalCurrency="VUV", Nationality="Ni-Vanatu", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "WF", Name="WALLIS AND FUTUNA",NationalCurrency="XPF", Nationality="Wallisian and Futunan", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "WS", Name="SAMOA",NationalCurrency="WST",Nationality="Samoan", CreatedBy="System",  CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "YE", Name="YEMEN",NationalCurrency="YER", Nationality="Yemenis", CreatedBy="System",  CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "YT", Name="MAYOTTE",NationalCurrency="EUR", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "ZA", Name="SOUTH AFRICA",NationalCurrency="ZAR", Nationality="South African", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "ZM", Name="ZAMBIA",NationalCurrency="ZMW", Nationality="Zambian", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null },
                new BtCountry { CountryCode = "ZW", Name="ZIMBABWE",NationalCurrency="ZWL", Nationality="Zimbabwean", CreatedBy="System", CreatedDate=DateTime.Now,  ModifiedBy=null, LastModified = null }
            };
            _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            _context.BtCountry.AddRange(country);
            _context.Database.OpenConnection();
            try
            {
                _context.SaveChanges();
            }
            finally
            {
                _context.Database.CloseConnection();
            }

            if (_context.BtState.Any())
                return; //DB has been seeded with language data
            var states = new BtState[]
            {
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Not Available", StateCode ="N/A" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Akwa Ibom", StateCode ="01" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Anambra", StateCode ="02" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Bauchi", StateCode ="03" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Adamawa", StateCode ="04" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Benue", StateCode ="05" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Borno", StateCode ="06" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Cross River", StateCode ="07" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="FCT", StateCode ="08" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Delta", StateCode ="09" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Imo", StateCode ="10" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Kaduna", StateCode ="11" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Kano", StateCode ="12" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Katsina", StateCode ="13" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Kwara", StateCode ="14" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Lagos", StateCode ="15" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Niger", StateCode ="16" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Ogun", StateCode ="17" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Ondo", StateCode ="18" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Oyo", StateCode ="19" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Plateau", StateCode ="20" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Rivers", StateCode ="21" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Sokoto", StateCode ="22" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Abia", StateCode ="23" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Edo", StateCode ="24" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Enugu", StateCode ="25" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Jigawa", StateCode ="26" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Kebbi", StateCode ="27" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Kogi", StateCode ="28" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Osun", StateCode ="29" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Taraba", StateCode ="30" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Yobe", StateCode ="31" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Bayelsa", StateCode ="32" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Ebonyi", StateCode ="33" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Ekiti", StateCode ="34" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Gombe", StateCode ="35" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Nassarawa", StateCode ="36" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                new BtState{ CountryId = country.Single( i => i.Name == "NIGERIA").CountryId, Name ="Zamfara", StateCode ="37" , CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  }
            };
            _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            _context.BtState.AddRange(states);
            _context.Database.OpenConnection();
            try
            {
                _context.SaveChanges();
            }
            finally
            {
                _context.Database.CloseConnection();
            }

            if (_context.BtLga.Any())
                return; //DB has been seeded with Local Government data
            var lgas = new BtLga[]
            {
                  new BtLga{  Name ="Not Available" , LgaCode ="N/A" , StateCode = "N/A",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Eket", LgaCode ="01.03" , StateCode = "01",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Essien-Udim", LgaCode ="01.05" , StateCode = "01",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Uyo", LgaCode ="01.29" , StateCode = "01",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Aguata", LgaCode ="02.01" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Anambra-East", LgaCode ="02.02" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Anaocha", LgaCode ="02.04" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Awka-South", LgaCode ="02.06" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Dunukofia" , LgaCode ="02.08" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ekwusigo" , LgaCode ="02.09" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Idemili-North" , LgaCode ="02.10" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Idemili-South" , LgaCode ="02.11" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Njikoka" , LgaCode ="02.12" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Nnewi-North" , LgaCode ="02.13" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Nnewi-South", LgaCode ="02.14 " , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ogbaru" , LgaCode ="02.15" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Onitsha-North" , LgaCode ="02.16" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Onitsha-South" , LgaCode ="02.17" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Oyi" , LgaCode ="02.20" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ihiala" , LgaCode ="02.21" , StateCode = "02",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Alkaleri" , LgaCode ="03.01" , StateCode = "03",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Darazo" , LgaCode ="03.04" , StateCode = "03",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ningi" , LgaCode ="03.06" , StateCode = "03",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Itas-Gadau" , LgaCode ="03.09" , StateCode = "03",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Katagum" , LgaCode ="03.11" , StateCode = "03",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kirfi" , LgaCode ="03.12" , StateCode = "03",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Misau" , LgaCode ="03.13" , StateCode = "03",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Warji" , LgaCode ="03.16" , StateCode = "03",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Zakki" , LgaCode ="03.17" , StateCode = "03",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Bauchi" , LgaCode ="03.19" , StateCode = "03",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Demsa" , LgaCode ="04.01" , StateCode = "04",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Fufure" , LgaCode ="04.02" , StateCode = "04",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Gombi" , LgaCode ="04.05" , StateCode = "04",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Girei" , LgaCode ="04.06" , StateCode = "04",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Hong" , LgaCode ="04.07" , StateCode = "04",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Michika" , LgaCode ="04.13" , StateCode = "04",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Numan" , LgaCode ="04.16",StateCode = "04",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Yola-South", LgaCode ="04.21",StateCode = "04",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Gboko", LgaCode ="05.05",StateCode = "05",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Makurdi", LgaCode ="05.13",StateCode = "05",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Okpokwu", LgaCode ="05.15",StateCode = "05",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Oturkpo", LgaCode ="05.17",StateCode = "05",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Obi", LgaCode ="05.22" , StateCode = "05",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ogbadigbo", LgaCode ="05.23",StateCode = "05",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Bama", LgaCode ="06.03",StateCode = "06",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Maiduguri", LgaCode ="06.21",StateCode = "06",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ngala", LgaCode ="06.25",StateCode = "06",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Calabar-Municipal", LgaCode ="07.07",StateCode = "07",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Calabar-South", LgaCode ="07.08",StateCode = "07",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ikom", LgaCode ="07.10",StateCode = "07",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Obudu", LgaCode ="07.13",StateCode = "07",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ogoja", LgaCode ="07.15",StateCode = "07",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Gwagalada", LgaCode ="08.03",StateCode = "08",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kuje", LgaCode ="08.04",StateCode = "08",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kwali" , LgaCode ="08.05",StateCode = "08",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Municipal Area Council" , LgaCode ="08.06",StateCode = "08",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Aniocha-North" , LgaCode ="09.01",StateCode = "09",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Aniocha-South" , LgaCode ="09.02",StateCode = "09",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Bomadi" , LgaCode ="09.03",StateCode = "09",CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ika-South" , LgaCode ="   09.08",StateCode = "09",CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Isoko-North" , LgaCode =" 09.09",StateCode = "09",CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Isoko-South" , LgaCode =" 09.10",StateCode = "09",CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ndokwa-East" , LgaCode =" 09.11",StateCode = "09",CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Oshimili-North" , LgaCode ="09.12",StateCode = "09",CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Oshimili-South" , LgaCode ="09.13",StateCode = "09",CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ughelli-North" , LgaCode ="09.17",StateCode = "09",CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ughelli-South" , LgaCode ="09.18",StateCode = "09",CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Uvwie" , LgaCode ="09.20",StateCode = "09",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Warri-North" , LgaCode =" 09.21",StateCode = "09",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Warri-South" , LgaCode =" 09.22",StateCode = "09",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Okpe" , LgaCode ="09.24",StateCode = "09",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Aboh-Mbaise" , LgaCode ="10.01",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ahiazu-Mbaise" , LgaCode ="10.02",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ehime-Mbano" , LgaCode ="10.03",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ezinihitte" , LgaCode ="10.04",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ideato-North" , LgaCode ="10.05",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ideato-South" , LgaCode ="10.06",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ihitte-Uboma" , LgaCode ="10.07",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Isu" , LgaCode ="10.10",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Mbaitoli" , LgaCode ="10.11",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ngor-Okpala" , LgaCode ="10.12",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Njaba" , LgaCode ="10.13",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Nkwerre" , LgaCode ="10.15",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ohaji-Egbema" , LgaCode ="10.18",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Okigwe" , LgaCode ="10.19",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Orlu" , LgaCode ="10.20",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Orsu" , LgaCode ="10.21",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Oru-East" , LgaCode ="10.22",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Owerri Municipal" , LgaCode ="10.24",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Owerri North" , LgaCode ="10.25",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Owerri West" , LgaCode ="10.26",StateCode = "10",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Giwa" , LgaCode ="11.03",StateCode = "11",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Jaba" , LgaCode ="11.06",StateCode = "11",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Jemaa" , LgaCode ="11.07",StateCode = "11",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kaduna North" , LgaCode ="11.09",StateCode = "11",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kaduna South" , LgaCode ="11.10",StateCode = "11",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kagarko" , LgaCode ="11.11",StateCode = "11",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kaura" , LgaCode ="11.13",StateCode = "11",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Lere" , LgaCode ="11.17",StateCode = "11",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Makarfi" , LgaCode ="11.18",StateCode = "11",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Sanga" , LgaCode ="11.20",StateCode = "11",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Zango-Kataf" , LgaCode ="11.22",StateCode = "11",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Zaria" , LgaCode ="11.23",StateCode = "11",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Dambatta" , LgaCode ="12.08",StateCode = "12",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Gwarzo" , LgaCode ="12.20",StateCode = "12",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kano Municipal", LgaCode ="12.22",StateCode = "12",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kibiya", LgaCode ="12.24",StateCode = "12",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Wudil", LgaCode ="12.45",StateCode = "12",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Batsari", LgaCode ="13.03",StateCode = "13",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Dan Musa", LgaCode ="13.09",StateCode = "13",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Daura", LgaCode ="13.10",StateCode = "13",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Faskari", LgaCode ="13.13",StateCode = "13",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Katsina", LgaCode ="13.21",StateCode = "13",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Malumfashi", LgaCode ="13.25",StateCode = "13",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ifelodun", LgaCode ="14.05",StateCode = "14",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Illorin West", LgaCode ="14.07",StateCode = "14",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Irepodun", LgaCode ="14.08",StateCode = "14",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Offa", LgaCode ="14.12",StateCode = "14",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Oke-Ero", LgaCode ="14.13",StateCode = "14",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Agege", LgaCode ="15.01",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ajeromi Ifelodun", LgaCode ="15.02",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Alimosho", LgaCode ="15.03",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Amuwo-Odofin", LgaCode ="15.04",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Apapa", LgaCode ="15.05",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Badagry", LgaCode ="15.06",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Eti-Osa", LgaCode ="15.08",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ibeju Lekki", LgaCode ="15.09",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ifako-Ijaiye", LgaCode ="15.10",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ikeja", LgaCode ="15.11",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ikorodu", LgaCode ="15.12",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kosofe", LgaCode ="15.13",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Lagos Island", LgaCode ="15.14",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Lagos Mainland", LgaCode ="15.15",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Mushin", LgaCode ="15.16",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ojo", LgaCode ="15.17",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Oshodi-Isolo", LgaCode ="15.18",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Shomolu", LgaCode ="15.19",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Surulere", LgaCode ="15.20",StateCode = "15",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Agaie", LgaCode ="16.01",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Agwara", LgaCode ="16.02",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Bida", LgaCode ="16.03",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Borgu", LgaCode ="16.04",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Bosso", LgaCode ="16.05",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Minna", LgaCode ="16.06",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Edati", LgaCode ="16.07",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Gbako", LgaCode ="16.08",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Katcha", LgaCode ="16.10",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kontagora",LgaCode ="16.11",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Lapai", LgaCode ="16.12",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Lavun", LgaCode ="16.13",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Mariga", LgaCode ="16.15",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Mashegu", LgaCode ="16.16",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Mokwa", LgaCode ="16.17",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Muya", LgaCode ="16.18",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Rafi", LgaCode ="16.20",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Rijau", LgaCode ="16.21",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Shiroro", LgaCode ="16.22",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Suleja", LgaCode ="16.23",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Wushishi", LgaCode ="16.25",StateCode = "16",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Abeokuta South", LgaCode ="17.02",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ado-Odo Ota", LgaCode ="17.03",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ifo", LgaCode ="17.07",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ijebu East", LgaCode ="17.08",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ijebu North", LgaCode ="17.09",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ijebu North East", LgaCode ="17.10",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ijebu-Ode", LgaCode ="17.11",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ikenne", LgaCode ="17.12",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Obafemi-Owode", LgaCode ="17.15",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ogun Waterside", LgaCode ="17.16",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Odeda", LgaCode ="17.17",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Odogbolu", LgaCode ="17.18",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Remo North", LgaCode ="17.19",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Sagamu", LgaCode ="17.20",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Yewa South", LgaCode ="17.21",StateCode = "17",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Akoko North East", LgaCode ="18.01",StateCode = "18",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Akoko South East", LgaCode ="18.03",StateCode = "18",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Akoko South West", LgaCode ="18.04",StateCode = "18",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Akure South", LgaCode ="18.06",StateCode = "18",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ese-Odo", LgaCode ="18.07",StateCode = "18",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Idanre", LgaCode ="18.08",StateCode = "18",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ifedore", LgaCode ="18.09",StateCode = "18",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ile-Oluji", LgaCode ="18.11",StateCode = "18",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Okeigbo", LgaCode ="18.12",StateCode = "18",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Irele", LgaCode ="18.13",StateCode = "18",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Okitipupa", LgaCode ="18.15",StateCode = "18",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ondo East", LgaCode ="18.16",StateCode = "18",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Akinyele", LgaCode ="19.02",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Atiba", LgaCode ="19.03",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Egbeda", LgaCode ="19.04",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ibadan Central", LgaCode ="19.05",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ibadan North West", LgaCode ="19.06",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ibadan South East", LgaCode ="19.07",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ibarapa Central", LgaCode ="19.09",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ibarapa East", LgaCode ="19.10",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ibarapa North", LgaCode ="19.11",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ido", LgaCode ="19.12",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Iseyin", LgaCode ="19.14",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Itesiwaju", LgaCode ="19.15",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kajola", LgaCode ="19.17",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ogbomosho South", LgaCode ="19.19",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Orelope", LgaCode ="19.24",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ori-Ire", LgaCode ="19.25",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Oyo East", LgaCode ="19.26",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Oyo West", LgaCode ="19.27",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Saki East", LgaCode ="19.28",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Saki West", LgaCode ="19.29",StateCode = "19",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Bokkos", LgaCode ="20.03",StateCode = "20",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Jos North", LgaCode ="20.05",StateCode = "20",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Jos South", LgaCode ="20.06",StateCode = "20",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Langtang South", LgaCode ="20.10",StateCode = "20",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Mangu", LgaCode ="20.11",StateCode = "20",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Pankshin", LgaCode ="20.13",StateCode = "20",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Riyom", LgaCode ="20.15",StateCode = "20",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Wase", LgaCode ="20.17",StateCode = "20",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Bonny", LgaCode ="21.07",StateCode = "21",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Eleme", LgaCode ="21.10",StateCode = "21",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Khana", LgaCode ="21.14",StateCode = "21",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Okrika", LgaCode ="21.18",StateCode = "21",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Port-Harcourt", LgaCode ="21.22",StateCode = "21",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Dange-shnsi", LgaCode ="22.03",StateCode = "22",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Goronyo", LgaCode ="22.05",StateCode = "22",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Gawabawa", LgaCode ="22.07",StateCode = "22",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Sokoto North", LgaCode ="22.16",StateCode = "22",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Yabo", LgaCode ="22.23",StateCode = "22",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Aba-North", LgaCode ="23.01",StateCode = "23",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Aba South", LgaCode ="23.02",StateCode = "23",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Arochukwu", LgaCode ="23.03",StateCode = "23",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Bende", LgaCode ="23.04",StateCode = "23",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Isiala Ngwa South", LgaCode ="23.07",StateCode = "23",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Isuikwuato", LgaCode ="23.08",StateCode = "23",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ohafia", LgaCode ="23.10",StateCode = "23",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ukwa East", LgaCode ="23.13",StateCode = "23",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Umuahia North", LgaCode ="23.15",StateCode = "23",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Esan North East", LgaCode ="24.01",StateCode = "24",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Esan Central", LgaCode ="24.02",StateCode = "24",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Esan West", LgaCode ="24.03",StateCode = "24",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Etsako Central", LgaCode ="24.06",StateCode = "24",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Etsako West", LgaCode ="24.07",StateCode = "24",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Oredo", LgaCode ="24.09",StateCode = "24",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ovia North-East", LgaCode ="24.11",StateCode = "24",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Uhunmwonde", LgaCode ="24.13",StateCode = "24",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Etsako East", LgaCode ="24.14",StateCode = "24",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Esan South-East", LgaCode ="24.15",StateCode = "24",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Owan East", LgaCode ="24.16",StateCode = "24",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Enugu South", LgaCode ="25.01",StateCode = "25",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Igbo-Eze South", LgaCode ="25.02",StateCode = "25",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Enugu North", LgaCode ="25.03",StateCode = "25",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ezeagu", LgaCode ="25.07",StateCode = "25",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Nsukka", LgaCode ="25.10",StateCode = "25",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Igbo-Etiti", LgaCode ="25.11",StateCode = "25",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Udenu", LgaCode ="25.16",StateCode = "25",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Awgu", LgaCode ="25.17",StateCode = "25",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Babura", LgaCode ="26.02",StateCode = "26",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Birnin Kudu", LgaCode ="26.03",StateCode = "26",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Dutse", LgaCode ="26.06",StateCode = "26",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Hadejia", LgaCode ="26.13",StateCode = "26",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kafin Hausa", LgaCode ="26.15",StateCode = "26",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kaugama Kazaure ", LgaCode ="26.16",StateCode = "26",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Maigatari", LgaCode ="26.19",StateCode = "26",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ringim", LgaCode ="26.22",StateCode = "26",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Taura", LgaCode ="26.25",StateCode = "26",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Aleiro", LgaCode ="27.01",StateCode = "27",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Argungu", LgaCode ="27.03",StateCode = "27",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Birnin-Kebbi", LgaCode ="27.06",StateCode = "27",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Dandi", LgaCode ="27.08",StateCode = "27",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Jega", LgaCode ="27.11",StateCode = "27",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Yauri", LgaCode ="27.20",StateCode = "27",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Zuru", LgaCode ="27.21",StateCode = "27",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Dekina", LgaCode ="28.05",StateCode = "28",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Idah", LgaCode ="28.07",StateCode = "28",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ijumu", LgaCode ="28.09",StateCode = "28",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kabba-Bunu", LgaCode ="28.10",StateCode = "28",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Lokoja", LgaCode ="28.12",StateCode = "28",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ofu", LgaCode ="28.14",StateCode = "28",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Okene", LgaCode ="28.17",StateCode = "28",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Yagba East", LgaCode ="28.20",StateCode = "28",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Aiyedire", LgaCode ="29.02",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Atakumosa East", LgaCode ="29.03",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ede North", LgaCode ="29.07",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ede South", LgaCode ="29.08",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ejigbo", LgaCode ="29.10",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ife Central", LgaCode ="29.11",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ifelodun", LgaCode ="29.16",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ila", LgaCode ="29.17",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ilesha East", LgaCode ="29.18",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ilesha West", LgaCode ="29.19",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Irepodun", LgaCode ="29.20",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Irewole", LgaCode ="29.21",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Iwo", LgaCode ="29.23",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Obokun", LgaCode ="29.24",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Odo-Otin", LgaCode ="29.25",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Oriade", LgaCode ="29.28",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Osogbo", LgaCode ="29.30",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ogbaagbaa Olaoluwa", LgaCode ="29.34",StateCode = "29",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Jalingo", LgaCode ="30.07",StateCode = "30",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Karin-Lamido", LgaCode ="30.08",StateCode = "30",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Takum", LgaCode ="30.12",StateCode = "30",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Wukari", LgaCode ="30.14",StateCode = "30",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Bade", LgaCode ="31.01",StateCode = "31",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Yenegoa", LgaCode ="32.08",StateCode = "32",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Afikpo South", LgaCode ="33.01",StateCode = "33",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Abakaliki", LgaCode ="33.05",StateCode = "33",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ivo", LgaCode ="33.12",StateCode = "33",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Izzi", LgaCode ="33.13",StateCode = "33",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ado", LgaCode ="34.01",StateCode = "34",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ekiti-West", LgaCode ="34.03",StateCode = "34",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ekiti South West", LgaCode ="34.05",StateCode = "34",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ijero", LgaCode ="34.08",StateCode = "34",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ido Osi", LgaCode ="34.09",StateCode = "34",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Oye", LgaCode ="34.10",StateCode = "34",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Ikole", LgaCode ="34.11",StateCode = "34",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Efon", LgaCode ="34.14",StateCode = "34",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Billiri", LgaCode ="35.03",StateCode = "35",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kaltungo", LgaCode ="35.05",StateCode = "35",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Gombe", LgaCode ="35.09",StateCode = "35",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Karu", LgaCode ="36.04",StateCode = "36",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Keffi", LgaCode ="36.06",StateCode = "36",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Kokona", LgaCode ="36.07",StateCode = "36",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Lafia", LgaCode ="36.08",StateCode = "36",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Nasarawa", LgaCode ="36.09",StateCode = "36",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Bakura", LgaCode ="37.02",StateCode = "37",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Bungudu", LgaCode ="37.05",StateCode = "37",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Gusau", LgaCode ="37.07",StateCode = "37",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Namoda", LgaCode ="37.09",StateCode = "37",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Shinkafi", LgaCode ="37.12",StateCode = "37",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  },
                  new BtLga{  Name ="Zurmi", LgaCode ="   37.15",StateCode = "37",  CreatedDate=DateTime.Now, CreatedBy="System", ModifiedBy = null, LastModified = null,  }
            };
            _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            _context.BtLga.AddRange(lgas);
            _context.Database.OpenConnection();
            try
            {
                _context.SaveChanges();
            }
            finally
            {
                _context.Database.CloseConnection();
            }
        }

        public void AutoSeedEmailTemplates()
        {
            //Email Template root path
            var currentDirectory = Directory.GetCurrentDirectory();
            string[] paths = new string[] { currentDirectory, "Templates", "EmailTemplates\\" };
            string path = Path.Combine(paths);

            string resetPwdTemplate = new StreamReader(string.Format("{0}ResetPassword.txt", path)).ReadToEnd();
            string confirmEmailTemplate = new StreamReader(string.Format("{0}ConfirmEmailAddress.txt", path)).ReadToEnd();
            string welcomeEmailTemplate = new StreamReader(string.Format("{0}WelcomeEmailAddress.txt", path)).ReadToEnd();
            string invoiceEmailTemplate = new StreamReader(string.Format("{0}SubscriptionInvoice.txt", path)).ReadToEnd();
            string WelcomeEmailTemplate = new StreamReader(string.Format("{0}MerchantWelcomeEmail.txt", path)).ReadToEnd();
            string PatientAppointmentBooking = new StreamReader(string.Format("{0}AppointmentBooking.txt", path)).ReadToEnd();
            string DoctorAppointmentBooking = new StreamReader(string.Format("{0}DoctorAppointmentBooking.txt", path)).ReadToEnd();
            string affiliateagrementTemplate = new StreamReader(string.Format("{0}PatientAgreementTemplate.txt", path)).ReadToEnd();
            string applicationLiveTemplate = new StreamReader(string.Format("{0}ApplicationLive.txt", path)).ReadToEnd();
            string zoomMeetingTemplate = new StreamReader(string.Format("{0}ZoomMeetingNotification.txt", path)).ReadToEnd();

            if (_context.TEmailtemplate.Any()) return; //DB has been seeded with Email Templates Already


            var emailTemplates = new TEmailtemplate[]
            {
                new TEmailtemplate{  Code = "RST_PWD", Name = "Reset Password", Subject = "myFacility - You Initiated a Password Reset!", Body = resetPwdTemplate,  CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
                new TEmailtemplate{  Code = "CONF_EML", Name = "Confirm Email Address", Subject = "myFacility - Please Confirm Your Email Address", Body = confirmEmailTemplate,  CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
                new TEmailtemplate{  Code = "SUBSC_INVC", Name = "Subscription Invoice", Subject = "Thank you for subscribing to myFacility - Here's your invoice.", Body = invoiceEmailTemplate,  CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
                new TEmailtemplate{  Code = "WLCM_EML", Name = "Welcome Email", Subject = "Welcome aboard myFacility - Anything to learn!", Body = welcomeEmailTemplate,  CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
                new TEmailtemplate{ Code = "PAT_AGRT", Name = "Patient Agreement Template", Subject = "Patient Agreement Template", Body = affiliateagrementTemplate,  CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
                new TEmailtemplate{  Code = "PATAPT_BKN", Name = "Patient Appointment Booking", Subject = "Patient Appointment Booking", Body = PatientAppointmentBooking,  CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
                new TEmailtemplate{  Code = "DOCAPT_BKN", Name = "Doctor Appointment Booking", Subject = "Doctor Appointment Booking", Body = DoctorAppointmentBooking,  CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
                new TEmailtemplate{  Code = "APPL_LIVE", Name = "Application Live", Subject = "Your profile has been created on Germeni myFacilityicine", Body = applicationLiveTemplate,  CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
                new TEmailtemplate{  Code = "ZM_MTN", Name = "Consultation Meeting Link", Subject = "Consultation Meeting Link", Body = zoomMeetingTemplate,  CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
            };

            _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            _context.TEmailtemplate.AddRange(emailTemplates);
            _context.Database.OpenConnection();

            try
            {
                _context.SaveChanges();
            }
            finally
            {
                _context.Database.CloseConnection();
            }
        }

        public void AutoSeedEmailToken()
        {
            if (_context.TEmailToken.Any())
                return; //DB has been seeded with Enrolee Types
            int i = 1;
            var emailtoken = new TEmailToken[]
           {
                new TEmailToken {  EmailtokenId= i++ , EmailToken = "[EMAIL]",PreviewName ="EMAIL",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
                new TEmailToken {  EmailtokenId= i++ , EmailToken = "[FIRSTNAME]",PreviewName ="FIRSTNAME",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
                new TEmailToken {  EmailtokenId= i++ , EmailToken = "[LASTNAME]",PreviewName ="LASTNAME",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
                new TEmailToken {  EmailtokenId= i++ , EmailToken = "[MIDDLENAME]",PreviewName ="MIDDLENAME",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
                new TEmailToken {  EmailtokenId= i++ , EmailToken = "[AGE]",PreviewName ="AGE",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
                new TEmailToken {  EmailtokenId= i++ , EmailToken = "[NATIONALITY]",PreviewName ="NATIONALITY",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
                new TEmailToken {  EmailtokenId= i++ , EmailToken = "[MERCHANT_FULLNAME]",PreviewName ="MERCHANT_FULLNAME",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
           };
            _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            _context.TEmailToken.AddRange(emailtoken);
            _context.Database.OpenConnection();
            try
            {
                _context.SaveChanges();
            }
            finally
            {
                _context.Database.CloseConnection();
            }
        }

        public void AutoSeedSmsTemplates()
        {
            //Email Template root path
            var currentDirectory = Directory.GetCurrentDirectory();
            string[] paths = new string[] { currentDirectory, "Templates", "SmsTemplates\\" };
            string path = Path.Combine(paths);

            string appointmentReminderSmsTemplate = new StreamReader(string.Format("{0}AppointmentReminder.txt", path)).ReadToEnd();
            string appointmentBookingSmsTemplate = new StreamReader(string.Format("{0}AppointmentBooking.txt", path)).ReadToEnd();
            string appointmentCancelationSmsTemplate = new StreamReader(string.Format("{0}AppointmentCancelation.txt", path)).ReadToEnd();
            string appointmentReschedulingSmsTemplate = new StreamReader(string.Format("{0}AppointmentRescheduling.txt", path)).ReadToEnd();
            string patientWelcomeSmsTemplate = new StreamReader(string.Format("{0}PatientRegistration.txt", path)).ReadToEnd();
            string resetPwdTemplate = new StreamReader(string.Format("{0}ResetPassword.txt", path)).ReadToEnd();

            if (_context.TSmstemplate.Any()) return; //DB has been seeded with Email Templates Already
            var smsTemplates = new TSmstemplate[]
            {
               new TSmstemplate{ Code = "APT_RMD", Name = "Appointment Reminder Template",   Sender = "Appointment",  Message = appointmentReminderSmsTemplate, CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
               new TSmstemplate{ Code = "APT_BKN", Name = "Appointment Booking Template", Sender = "Genesys", Message = appointmentBookingSmsTemplate, CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
               new TSmstemplate{ Code = "APT_CSL", Name = "Appointment Cancelation Template", Sender = "Genesys", Message = appointmentCancelationSmsTemplate, CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
               new TSmstemplate{ Code = "APT_RSC", Name = "Appointment Rescheduling Template", Sender = "Genesys", Message = appointmentReschedulingSmsTemplate, CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
               new TSmstemplate{ Code = "PWLCM_SMS", Name = "Patient Welcome SMS", Sender = "Genesys", Message = patientWelcomeSmsTemplate, CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },
               new TSmstemplate{  Code = "RST_PWD", Name = "Reset Password", Sender = "Genesys", Message = resetPwdTemplate,  CreatedBy = "System", CreatedDate = DateTime.Now, IsActive = true, LastModified = null, ModifiedBy = null },

            };

            _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            _context.TSmstemplate.AddRange(smsTemplates);
            _context.Database.OpenConnection();

            try
            {
                _context.SaveChanges();
            }
            finally
            {
                _context.Database.CloseConnection();
            }
        }

        public void AutoSeedSmsToken()
        {
            if (_context.TSmsToken.Any())
                return; //DB has been seeded with Enrolee Types
            var smstoken = new TSmsToken[]
           {
                new TSmsToken { SmsToken = "[EMAIL]",PreviewName ="EMAIL",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
                new TSmsToken { SmsToken = "[FIRSTNAME]",PreviewName ="FIRSTNAME",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
                new TSmsToken { SmsToken = "[LASTNAME]",PreviewName ="LASTNAME",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
                new TSmsToken { SmsToken = "[MIDDLENAME]",PreviewName ="MIDDLENAME",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
                new TSmsToken { SmsToken = "[AGE]",PreviewName ="AGE",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
                new TSmsToken { SmsToken = "[NATIONALITY]",PreviewName ="NATIONALITY",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
                new TSmsToken { SmsToken = "[RELIGION]",PreviewName ="RELIGION",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
                new TSmsToken { SmsToken = "[PATIENTNO]",PreviewName ="PATIENTNO",  IsActive = true, CreatedBy="System", CreatedDate = DateTime.Now},
           };
            _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            _context.TSmsToken.AddRange(smstoken);
            _context.Database.OpenConnection();
            try
            {
                _context.SaveChanges();
            }
            finally
            {
                _context.Database.CloseConnection();
            }
        }

        public void AutoSeedSystemConfigurations()
        {
            if (_context.BtSysConfiguration.Any())
                return; //DB has been seeded with  System Configurations

            var sysconfig = new BtSysConfiguration[]
           {
                new BtSysConfiguration {  ConfigId= 5006,  DefaultValue="90",  ConfigValue="", Enabled = 1, ModifiedBy="System", LastModified = DateTime.Now, ConfigValueDesc ="The expiry date for password on the system in days" },
                new BtSysConfiguration {  ConfigId= 5017,  DefaultValue="24",  ConfigValue="", Enabled = 1, ModifiedBy="System", LastModified = DateTime.Now, ConfigValueDesc ="The Minimum Password reset period." },
                new BtSysConfiguration {  ConfigId= 5021,  DefaultValue="C:\\Temp\\Logo",  ConfigValue="", Enabled = 1, ModifiedBy="System", LastModified = DateTime.Now, ConfigValueDesc ="The Logo Storage Path" },
                new BtSysConfiguration {  ConfigId= 5036,  DefaultValue="5",  ConfigValue="", Enabled = 1, ModifiedBy="System", LastModified = DateTime.Now, ConfigValueDesc ="The Maximum Logo Size Merchant can upload" },
                new BtSysConfiguration {  ConfigId= 5037,  DefaultValue=".png,.jpeg",  ConfigValue="", Enabled = 1, ModifiedBy="System", LastModified = DateTime.Now, ConfigValueDesc ="The Accepted Logo Format" },
                new BtSysConfiguration {  ConfigId= 6000,  DefaultValue=string.Empty,  ConfigValue=string.Empty, Enabled = 1, ModifiedBy="System", LastModified = DateTime.Now, ConfigValueDesc ="Web Application URL" },
                new BtSysConfiguration {  ConfigId= 7000,  DefaultValue="30",  ConfigValue=string.Empty, Enabled = 1, ModifiedBy="System", LastModified = DateTime.Now, ConfigValueDesc ="The maximum time patient can reserve appointment before online payment in mins" },
                new BtSysConfiguration {  ConfigId= 8000,  DefaultValue="5000.00",  ConfigValue=string.Empty, Enabled = 1, ModifiedBy="System", LastModified = DateTime.Now, ConfigValueDesc ="Flat Rate Hospital can charge patient for online service" }
           };
            _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            _context.BtSysConfiguration.AddRange(sysconfig);
            _context.Database.OpenConnection();
            try
            {
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.bt_sys_configuration ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.bt_sys_configuration OFF");
            }
            finally
            {
                _context.Database.CloseConnection();
            }
        }
    }
}