

using WCS.ClassDomain.Domains;
using WCS.DataLayer.Contex;
using WCS.InterfaceService.Interfaces;
using WCS.Service.ServiceBase;

namespace WCS.Service.Services
{
    public class CivilContractService : BaseService<CivilContract>, ICivilContractService
    {
        private readonly ContextWCS _ContextWCS;
        public CivilContractService(ContextWCS ContextWCS) : base(ContextWCS)
        {
            _ContextWCS = ContextWCS;
        }
    }
}
