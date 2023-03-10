

using WCS.ClassDomain.Domains;
using WCS.DataLayer.Contex;
using WCS.InterfaceService.Interfaces;
using WCS.Service.ServiceBase;

namespace WCS.Service.Services
{
    public class WornCarsService : BaseService<WornCars>, IWornCarsService
    {
        private readonly ContextWCS _ContextWCS;
        public WornCarsService(ContextWCS ContextWCS) : base(ContextWCS)
        {
            _ContextWCS = ContextWCS;
        }
    }
}
