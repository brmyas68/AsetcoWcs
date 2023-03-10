using WCS.DataLayer.Contex;
using WCS.InterfaceService.ExternalInterfaces;
using WCS.InterfaceService.Interfaces;
using WCS.InterfaceService.InterfacesBase;
using WCS.Service.ExternalServices;
using WCS.Service.Services;

namespace WCS.Service.ServiceBase
{
    public class UnitOfWorkWCSService : IUnitOfWorkWCSService
    {
        private readonly ContextWCS _ContextWCS;

        private IBank _BankService;
        private IBusinessService _BusinessService;
        private ICivilContractService _CivilContractService;
        private IFinanceService _FinanceService;
        private IOwnersService _OwnersService;
        private IWornCarsService _WornCarsService;
        private IWornCenterService _WornCenterService;
        private IWornMasterService _WornMasterService;
        private IProductService _ProductService;
        private IProductPriceService _ProductPriceService;
        private IProductGroupService _ProductGroupService;
        private IOrderService _OrderService;
        private IMessageService _MessageService;
        private IAuthenticationService _AuthenticationService;
        private IOrderTransactionService _OrderTransactionService;


        public UnitOfWorkWCSService(ContextWCS ContextWCS)
        {
            _ContextWCS = ContextWCS;
        }

        public IBank _IBank { get { return _BankService = _BankService ?? new BankService(); } }
        public IBusinessService _IBusinessService { get { return _BusinessService = _BusinessService ?? new BusinessService(_ContextWCS); } }

        public ICivilContractService _ICivilContractService { get { return _CivilContractService = _CivilContractService ?? new CivilContractService(_ContextWCS); } }

        public IFinanceService _IFinanceService { get { return _FinanceService = _FinanceService ?? new FinanceService(_ContextWCS); } }

        public IOwnersService _IOwnersService { get { return _OwnersService = _OwnersService ?? new OwnersService(_ContextWCS); } }

        public IWornCarsService _IWornCarsService { get { return _WornCarsService = _WornCarsService ?? new WornCarsService(_ContextWCS); } }

        public IWornCenterService _IWornCenterService { get { return _WornCenterService = _WornCenterService ?? new WornCenterService(_ContextWCS); } }
        public IWornMasterService _IWornMasterService { get { return _WornMasterService = _WornMasterService ?? new WornMasterService(_ContextWCS); } }

        public IProductService _IProductService { get { return _ProductService = _ProductService ?? new ProductService(_ContextWCS); } }
        public IProductGroupService _IProductGroupService { get { return _ProductGroupService = _ProductGroupService ?? new ProductGroupService(_ContextWCS); } }

        public IProductPriceService _IProductPriceService { get { return _ProductPriceService = _ProductPriceService ?? new ProductPriceService(_ContextWCS); } }

        public IOrderService _IOrderService { get { return _OrderService = _OrderService ?? new OrderService(_ContextWCS); } }

        public IMessageService _IMessageService { get { return _MessageService = _MessageService ?? new MessageService(_ContextWCS); } }

        public IAuthenticationService _IAuthenticationService { get { return _AuthenticationService = _AuthenticationService ?? new AuthenticationService(); } }

        public IOrderTransactionService _IOrderTransactionService { get { return _OrderTransactionService = _OrderTransactionService ?? new  OrderTransactionService(_ContextWCS); } }
       
        public int SaveChange_DataBase()
        {
            return _ContextWCS.SaveChanges();
        }

        public async Task<int> SaveChange_DataBase_Async()
        {
            return await _ContextWCS.SaveChangesAsync().ConfigureAwait(false);
        }

        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _ContextWCS.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
