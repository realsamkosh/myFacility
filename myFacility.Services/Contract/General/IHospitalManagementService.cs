using System.Threading.Tasks;
using myFacility.Models.DataObjects.Regulator;


namespace myFacility.Services.Contract
{
    public interface IHospitalManagementService
    {
        #region
        Task<string> CreateRegulator(RegulatorDTO obj);
        Task<RegulatorViewModel> GetHospitalDetails();
        Task<string> UpdateRegulator(RegulatorDTO model);
        Task<bool> Checkexist();
        #endregion
    }
}
