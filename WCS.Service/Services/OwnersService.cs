

using WCS.ClassDomain.Domains;
using WCS.DataLayer.Contex;
using WCS.InterfaceService.Interfaces;
using WCS.Service.ServiceBase;

namespace WCS.Service.Services
{
    public class OwnersService : BaseService<Owners>, IOwnersService
    {
        private readonly ContextWCS _ContextWCS;
        public OwnersService(ContextWCS ContextWCS) : base(ContextWCS)
        {
            _ContextWCS = ContextWCS;
        }
    }
}
