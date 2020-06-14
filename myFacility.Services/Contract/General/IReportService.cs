using System.Collections.Generic;
using System.Threading.Tasks;
using myFacility.Models.DataObjects.Report;

namespace myFacility.Services.Contract
{
    public interface IReportService
    {
        Task<IEnumerable<PurchaseHistoryViewModel>> GetPurchaseHistory();

    }
}
