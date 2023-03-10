
using System.Data;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs.Customs;
using WCS.ClassDTO.DTOs.Filters;
using WCS.InterfaceService.InterfacesBase;

namespace WCS.InterfaceService.Interfaces
{
    public interface IProductPriceService : IBaseService<ProductPrice>
    {
        Task<List<DtoProductPrice_>> GetAll_SP(int? ProductId, string AddDate);
        Task<DtoProductPrice_> GetPriceById_SP(int ProdPricId);
        Task<ProductPrice> GetLastProductPrice(int ProductId);
    }
}
