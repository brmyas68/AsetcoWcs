

using WCS.ClassDomain.Domains;
using WCS.InterfaceService.InterfacesBase;

namespace WCS.InterfaceService.Interfaces
{
    public interface IFinanceService : IBaseService<Finance>
    {
        Task<List<Finance>> GetAllFinanceByWMasterID(int WornMasterID);
    }
}
