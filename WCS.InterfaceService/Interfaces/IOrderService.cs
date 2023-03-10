

using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs.Customs;
using WCS.InterfaceService.InterfacesBase;

namespace WCS.InterfaceService.Interfaces
{
    public interface IOrderService : IBaseService<Order>
    {
        Task<List<DtoOrders>> GetAll_SP(System.Data.DataTable Dt, int UserID);

        Task<DtoOrders> GetByOrder_SP(int OrderID);
    }
}
