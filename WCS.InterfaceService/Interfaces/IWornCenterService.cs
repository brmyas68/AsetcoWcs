

using WCS.ClassDomain.Domains;
using WCS.InterfaceService.InterfacesBase;

namespace WCS.InterfaceService.Interfaces
{
    public interface IWornCenterService : IBaseService<WornCenter>
    {
        Task<List<WornCenter>> GetAllWornCenterByType(int WCenterType);
    }
}
