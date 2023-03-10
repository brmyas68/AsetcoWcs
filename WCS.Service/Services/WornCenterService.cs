

using Microsoft.EntityFrameworkCore;
using WCS.ClassDomain.Domains;
using WCS.DataLayer.Contex;
using WCS.InterfaceService.Interfaces;
using WCS.Service.ServiceBase;

namespace WCS.Service.Services
{
    public class WornCenterService : BaseService<WornCenter>, IWornCenterService
    {
        private readonly ContextWCS _ContextWCS;
        public WornCenterService(ContextWCS ContextWCS) : base(ContextWCS)
        {
            _ContextWCS = ContextWCS;
        }
        public async Task<List<WornCenter>> GetAllWornCenterByType(int WCenterType)
        {
            return await _ContextWCS.WornCenter.Where(W => W.WornCenter_Type == WCenterType).ToListAsync().ConfigureAwait(false);
        }
    }
}
