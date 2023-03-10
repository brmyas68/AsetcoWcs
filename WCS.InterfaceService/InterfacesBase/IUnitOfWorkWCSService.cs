

using WCS.ClassDTO.DTOs;
using WCS.InterfaceService.ExternalInterfaces;
using WCS.InterfaceService.Interfaces;

namespace WCS.InterfaceService.InterfacesBase
{
    public interface IUnitOfWorkWCSService : IDisposable
    {
        IBank _IBank { get; }
        IBusinessService _IBusinessService { get; }
        ICivilContractService _ICivilContractService { get; }
        IFinanceService _IFinanceService { get; }
        IOwnersService _IOwnersService { get; }
        IWornCarsService _IWornCarsService { get; }
        IWornCenterService _IWornCenterService { get; }
        IWornMasterService _IWornMasterService { get; }
        IProductService _IProductService { get; }
        IProductGroupService _IProductGroupService { get; }
        IProductPriceService _IProductPriceService { get; }
        IOrderService _IOrderService { get; }
        IMessageService _IMessageService { get; }
        IAuthenticationService _IAuthenticationService { get; }
        IOrderTransactionService _IOrderTransactionService { get; }

        int SaveChange_DataBase();
        Task<int> SaveChange_DataBase_Async();

    }
}
