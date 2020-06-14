using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using myFacility.Infrastructure;
using myFacility.Models.DataObjects.Regulator;
using myFacility.Models.Domains.Account;
using myFacility.Models.Domains.Regulator;
using myFacility.Services.Contract;
using myFacility.Utilities.AuthenticationUtility.AuthUser;
using myFacility.Utilities.Enums;
using myFacility.Utilities.ExceptionUtility;
using myFacility.Utilities.Extensions.Permission;

namespace myFacility.Services.Handler
{
    public class HospitalManagementService : IHospitalManagementService
    {
        private readonly authDbContext _context;
        private readonly ILogger<HospitalManagementService> _logger;
        private readonly IAuthUser _authUser;
   

        public HospitalManagementService(authDbContext context,
            ILogger<HospitalManagementService> logger, IAuthUser authUser)
        {
            _context = context;
            _logger = logger;
            _authUser = authUser;
          
        }

        public async Task<bool> Checkexist()
        {
            var check = await _context.TRegulator.AsNoTracking().FirstOrDefaultAsync();
            if (check == null)
            {
                return false;
            }
            else
                return true;
        }

        #region Regulator
        public async Task<string> CreateRegulator(RegulatorDTO obj)
        {
            string status = "";
            try
            {
                //Fetch all permissions
                var fetchallpermissions = Enum.GetValues(typeof(myFacilityPermissions)).Cast<myFacilityPermissions>()
                         .ToList();
                //Check if Course and Description exist
                var checkexist = await _context.TRegulator.AnyAsync(x => x.Name == obj.name);
                if (checkexist == false)
                {
                    TRegulator newrecord = new TRegulator
                    {
                        Name = obj.name,
                        Address = obj.address,
                        Email = obj.email,
                        RegCode = obj.regcode,
                        CreatedBy = _authUser.Name,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        LastModified = null,
                        ModifiedBy = null
                    };

                    _context.TRegulator.Add(newrecord);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        BtPermissions btPermissions = new BtPermissions
                        {
                            PermissionId = Guid.NewGuid().ToString(),
                            Permissions = fetchallpermissions.Where(x => x != myFacilityPermissions.AccessAll
                            && x != myFacilityPermissions.NotSet).PackPermissionsIntoString(),
                            IsActive = true,
                            RegId = newrecord.RegId,
                            CreatedBy = _authUser.Name,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = null,
                            LastModified = null
                        };

                        _context.BtPermissions.Add(btPermissions);
                        await _context.SaveChangesAsync();
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

        public async Task<RegulatorViewModel> GetHospitalDetails()
        {
            try
            {
                // FETCH oNE Course Record
                var fetchhosp = await _context.TRegulator.FirstOrDefaultAsync();

                if (fetchhosp != null)
                {
                    return new RegulatorViewModel
                    {
                        ////id = fetchhosp.Id,
                        ////location = fetchhosp.Location,
                        ////name = fetchhosp.Name,
                        ////status = fetchhosp.Status,
                        ////guid = fetchhosp.Guid,
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> UpdateRegulator(RegulatorDTO model)
        {
            string status = "";
            try
            {
                //Check if record exist not as same id
                var state = await _context.TRegulator.FirstOrDefaultAsync();
                state.RegId = state.RegId;
                state.Name = model.name;
                state.Address = model.address;
                state.Email = model.email;
                state.RegCode = model.regcode;
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
