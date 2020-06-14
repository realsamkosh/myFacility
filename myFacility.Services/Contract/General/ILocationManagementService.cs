using myFacility.Models.DataObjects.Location;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myFacility.Services.Contract
{
    public interface ILocationManagementService
    {
        //Country Services
        Task<IEnumerable<CountryViewModel>> GetCountries(bool status = true);
        Task<CountryViewModel> GetCountry(int id, bool status = true);
        Task<string> CreateCountry(CountryDTO obj);
        Task<string> UpdateCountry(CountryDTO obj, int id);
        Task<bool> DeleteCountry(int id, bool status);

        //State Services
        Task<IEnumerable<StateViewModel>> GetStates(bool status = true);
        Task<StateViewModel> GetState(int id, bool status = true);
        Task<int> GetStateId(string id);
        Task<string> CreateState(StateDTO obj);
        Task<string> UpdateState(StateDTO obj, int id);
        Task<bool> DeleteState(int id, bool status);

        //Lga Services
        Task<IEnumerable<LgaViewModel>> GetLgas(bool status = true);
        Task<LgaViewModel> GetLga(int id, bool status = true);
        Task<string> CreateLga(LgaDTO obj);
        Task<string> UpdateLga(LgaDTO obj, int id);
        Task<bool> DeleteLga(int id, bool status);
    }
}
