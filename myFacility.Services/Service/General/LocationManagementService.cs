using myFacility.Infrastructure;
using myFacility.Models.DataObjects.Location;
using myFacility.Models.Domains.Location;
using myFacility.Services.Contract;
using myFacility.Utilities.AuthenticationUtility.AuthUser;
using myFacility.Utilities.ExceptionUtility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myFacility.Services.Handler
{
    public class LocationManagementService : ILocationManagementService
    {
        private readonly authDbContext _context;
        private readonly ILogger<LocationManagementService> _logger;
        private readonly IAuthUser _authUser;

        public LocationManagementService(authDbContext context,
            ILogger<LocationManagementService> logger, IAuthUser authUser)
        {
            _context = context;
            _logger = logger;
            _authUser = authUser;
        }
        #region Country Services
        /// <summary>
        /// Create Country
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<string> CreateCountry(CountryDTO obj)
        {
            string status = "";
            try
            {
                //Check if Countrycode, NAME AND Nationality exist
                var checkexist = await _context.BtCountry
                    .AnyAsync(x => x.CountryCode == obj.countrycode ||
                    x.Name == obj.name || x.Nationality == obj.nationality);
                if (checkexist == false)
                {
                    BtCountry newrecord = new BtCountry
                    {
                        CountryCode = obj.countrycode,
                        Name = obj.name,
                        NationalCurrency = obj.nationalcurrency,
                        Nationality = obj.nationality,
                        CreatedBy = _authUser.Name,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        LastModified = null,
                        ModifiedBy = null
                    };

                    _context.BtCountry.Add(newrecord);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        status = "1";

                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.name);
                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }

        /// <summary>
        /// Get all countries base on status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CountryViewModel>> GetCountries(bool status = true)
        {
            try
            {
                // FETCH Active Country Record
                return await _context.BtCountry
                .Where(x => x.IsActive == status)
                .Select(c => new CountryViewModel
                {
                    countrycode = c.CountryCode,
                    countryid = c.CountryId,
                    name = c.Name,
                    nationalcurrency = c.NationalCurrency,
                    nationality = c.Nationality
                })
                .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get only one country
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<CountryViewModel> GetCountry(int id, bool status = true)
        {
            try
            {
                // FETCH oNE Country Record
                return await _context.BtCountry
                .Where(x => x.IsActive == status && x.CountryId == id)
                .Select(c => new CountryViewModel
                {
                    countrycode = c.CountryCode,
                    countryid = c.CountryId,
                    name = c.Name,
                    nationalcurrency = c.NationalCurrency,
                    nationality = c.Nationality
                })
                .FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update Country
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> UpdateCountry(CountryDTO obj, int id)
        {
            string status = "";
            try
            {
                //Check if record exist not as same id
                var checkexist = await _context.BtCountry
                   .AnyAsync(x => x.CountryId != id &&
                   (x.CountryCode == obj.countrycode ||
                   x.Name == obj.name || x.Nationality == obj.nationality));
                if (checkexist)
                {
                    var coutry = await _context.BtCountry
                    .FirstOrDefaultAsync(x => x.CountryId == id);
                    coutry.Name = obj.name;
                    coutry.NationalCurrency = obj.nationalcurrency;
                    coutry.Nationality = obj.nationality;
                    coutry.CountryCode = obj.countrycode;
                    coutry.ModifiedBy = _authUser.Name;
                    coutry.LastModified = DateTime.Now;
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        status = "1";
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.name);
                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }

        /// <summary>
        /// Delete country
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCountry(int id, bool status)
        {
            try
            {
                var coutry = await _context.BtCountry
                    .FirstOrDefaultAsync(x => x.CountryId == id);
                coutry.IsActive = status;
                coutry.ModifiedBy = _authUser.Name;
                coutry.LastModified = DateTime.Now;
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        #endregion

        #region State Services
        /// <summary>
        ///     Create State
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<string> CreateState(StateDTO obj)
        {
            string status = "";
            try
            {
                //Check Country
                var checkcountry = await _context.BtCountry.AsNoTracking().FirstOrDefaultAsync(x => x.CountryCode == obj.countrycode);
                if (checkcountry == null)
                {
                    status = "The country code is not valid.";
                    return status;
                }

                //Check Country
                var checkstate = await _context.BtState.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Name == obj.name && x.CountryId == checkcountry.CountryId);
                if (checkstate != null)
                {
                    status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.name);
                    return status;
                }

                //Assign State Code
                var getstate = await _context.BtState.AsNoTracking()
                    .Where(x => x.CountryId == checkcountry.CountryId && !x.StateCode.Contains("N/A"))
                    .Select(x => int.Parse(x.StateCode)).ToListAsync();

                var getLastID = (getstate.Count() > 0) ? getstate.Max() : 0;



                BtState newrecord = new BtState
                {
                    Name = obj.name,
                    StateCode = Convert.ToString(getLastID + 1),
                    CountryId = checkcountry.CountryId,
                    CreatedBy = _authUser.Name,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    LastModified = null,
                    ModifiedBy = null

                };
                _context.BtState.Add(newrecord);
                if (await _context.SaveChangesAsync() > 0)
                {
                    status = "1";

                    return status;
                }
                else
                {
                    status = ResponseErrorMessageUtility.RecordNotSaved;
                    return status;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }

        }
        /// <summary>
        /// Delete State
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteState(int id, bool status)
        {

            try
            {
                var coutry = await _context.BtState
                    .FirstOrDefaultAsync(x => x.StateId == id);
                coutry.IsActive = status;
                coutry.ModifiedBy = _authUser.Name;
                coutry.LastModified = DateTime.Now;
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        public async Task<StateViewModel> GetState(int id, bool status = true)
        {

            try
            {
                // Fetch one State record
                return await _context.BtState
                .Include(x => x.Country)
                .Where(x => x.IsActive == status && x.StateId == id)
                .Select(c => new StateViewModel
                {
                    stateid = c.StateId,
                    countrycode = c.Country.CountryCode,
                    countryname = c.Country.Name,
                    name = c.Name
                })

                 .FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<int> GetStateId(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StateViewModel>> GetStates(bool status = true)
        {

            try
            {
                // FETCH Active State Record
                return await _context.BtState
                    .Include(x => x.Country)
                .Where(x => x.IsActive == status)
                .Select(c => new StateViewModel
                {
                    stateid = c.StateId,
                    countrycode = c.Country.CountryCode,
                    countryname = c.Country.Name,
                    name = c.Name,
                    statecode = c.StateCode
                })
                .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update State Record
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> UpdateState(StateDTO obj, int id)
        {
            string status = "";
            try
            {
                //Check if record exist not as same id
                var checkexist = await _context.BtState.AnyAsync(x => x.StateId != id && (x.Name == obj.name));
                if (checkexist == false)
                {
                    var state = await _context.BtState.FirstOrDefaultAsync(x => x.StateId == id);
                    state.Name = obj.name;

                    //state.CountryCode = obj.countrycode;
                    state.ModifiedBy = _authUser.Name;
                    state.LastModified = DateTime.Now;
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        status = "1";
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.name);
                return status;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }
        #endregion


        #region Local goverment  Services
        public async Task<string> CreateLga(LgaDTO obj)
        {
            string status = "";
            try
            {
                //Check Country
                var checkcountry = await _context.BtLga.AsNoTracking().FirstOrDefaultAsync(x => x.StateCode == obj.statecode);
                if (checkcountry == null)
                {
                    status = "The state code is not valid.";
                    return status;
                }

                //Check Country
                var checkstate = await _context.BtLga.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Name == obj.name && x.StateCode == checkcountry.StateCode);
                if (checkstate != null)
                {
                    status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.name);
                    return status;
                }

                //Assign State Code
                var getstate = await _context.BtLga.AsNoTracking()
                    .Where(x => x.StateCode == checkcountry.StateCode && !x.StateCode.Contains("N/A"))
                    .Select(x => int.Parse(x.StateCode)).ToListAsync();

                var getLastID = (getstate.Count() > 0) ? getstate.Max() : 0;



                BtLga newrecord = new BtLga
                {
                    Name = obj.name,
                    LgaCode = Convert.ToString(getLastID + 1),
                    StateCode = checkcountry.StateCode,
                    CreatedBy = _authUser.Name,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    LastModified = null,
                    ModifiedBy = null

                };
                _context.BtLga.Add(newrecord);
                if (await _context.SaveChangesAsync() > 0)
                {
                    status = "1";

                    return status;
                }
                else
                {
                    status = ResponseErrorMessageUtility.RecordNotSaved;
                    return status;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }

        }
        /// <summary>
        /// Delete State
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteLga(int id, bool status)
        {

            try
            {
                var coutry = await _context.BtState
                    .FirstOrDefaultAsync(x => x.StateId == id);
                coutry.IsActive = status;
                coutry.ModifiedBy = _authUser.Name;
                coutry.LastModified = DateTime.Now;
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        public async Task<LgaViewModel> GetLga(int id, bool status = true)
        {

            try
            {
                // Fetch one State record
                return await _context.BtLga
                .Include(x => x.StateCodeNavigation)
                .Where(x => x.IsActive == status && x.LgaId == id)
                .Select(c => new LgaViewModel
                {
                    //stateid = c.StateId,
                    //countrycode = c.Country.CountryCode,
                    //countryname = c.Country.Name,
                    name = c.Name
                })

                 .FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<LgaViewModel>> GetLgas(bool status = true)
        {

            try
            {
                // FETCH Active State Record
                return await _context.BtLga.Include(x => x.StateCodeNavigation)
                .Where(x => x.IsActive == status)
                .Select(c => new LgaViewModel
                {
                    stateid = c.StateCodeNavigation.StateId,
                    lgacode = c.LgaCode,
                    lgaid = c.LgaId,
                    statecode = c.StateCode,
                    name = c.Name
                })
                .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update State Record
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> UpdateLga(LgaDTO obj, int id)
        {
            string status = "";
            try
            {
                //Check if record exist not as same id
                var checkexist = await _context.BtState.AnyAsync(x => x.StateId != id && (x.Name == obj.name));
                if (checkexist == false)
                {
                    var state = await _context.BtState.FirstOrDefaultAsync(x => x.StateId == id);
                    state.Name = obj.name;

                    //state.CountryCode = obj.countrycode;
                    state.ModifiedBy = _authUser.Name;
                    state.LastModified = DateTime.Now;
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        status = "1";
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.name);
                return status;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }
        #endregion
    }
}
