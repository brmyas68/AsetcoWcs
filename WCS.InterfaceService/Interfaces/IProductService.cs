
using System.Data;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs.Customs;
using WCS.ClassDTO.DTOs.Filters;
using WCS.InterfaceService.InterfacesBase;

namespace WCS.InterfaceService.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        Task<List<DtoBuyProducts_>> GetAll_SP(DataTable Dt);
        Task<List<Product>> GetAll(Int16 Type);
        Task<List<DtoBuyProducts_>> GetDetailProducts(List<int> ProductSID);

    }
}
