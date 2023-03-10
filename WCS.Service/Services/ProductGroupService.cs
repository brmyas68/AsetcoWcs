using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs.Customs;
using WCS.ClassDTO.DTOs.Filters;
using WCS.Common.StoredProcedure;
using WCS.DataLayer.Contex;
using WCS.InterfaceService.Interfaces;
using WCS.Service.ServiceBase;

namespace WCS.Service.Services
{
    public class ProductGroupService : BaseService<ProductGroup>, IProductGroupService
    {
        private readonly ContextWCS _ContextWCS;
        public ProductGroupService(ContextWCS ContextWCS) : base(ContextWCS)
        {
            _ContextWCS = ContextWCS;
        }

    }
}
