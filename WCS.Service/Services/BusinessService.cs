
using WCS.ClassDomain.Domains;
using WCS.DataLayer.Contex;
using WCS.InterfaceService.Interfaces;
using WCS.Service.ServiceBase;

namespace WCS.Service.Services
{
    public class BusinessService : BaseService<Business>, IBusinessService
    {
        private readonly ContextWCS _ContextWCS;
        public BusinessService(ContextWCS ContextWCS) : base(ContextWCS)
        {
            _ContextWCS = ContextWCS;
        }
    }
}
