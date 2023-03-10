

using Microsoft.EntityFrameworkCore;
using WCS.ClassDomain.Domains;
using WCS.DataLayer.Contex;
using WCS.InterfaceService.Interfaces;
using WCS.Service.ServiceBase;

namespace WCS.Service.Services
{
    public class FinanceService : BaseService<Finance>, IFinanceService
    {
        private readonly ContextWCS _ContextWCS;
        public FinanceService(ContextWCS ContextWCS) : base(ContextWCS)
        {
            _ContextWCS = ContextWCS;
        }
        public async Task<List<Finance>> GetAllFinanceByWMasterID(int WornMasterID)
        {
            return await _ContextWCS.Finance.Where(F => F.Finance_WornMasterID == WornMasterID).ToListAsync().ConfigureAwait(false);
        }
    }
}
