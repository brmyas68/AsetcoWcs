using Annex.InterfaceService.InterfacesBase;
using Annex.Service.ServiceBase;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WCS.Common.Authorization;
using UC.InterfaceService.InterfacesBase;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs.Customs;
using WCS.ClassDTO.DTOs.Filters;
using WCS.Common.Exceptions;
using WCS.Common.Mapping;
using WCS.InterfaceService.InterfacesBase;
using WCS.Common.Enum;
using WCS.ClassDTO.DTOs;

namespace WCS.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private IUnitOfWorkWCSService _UnitOfWorkWCSService; private IUnitOfWorkAnnexService _UnitOfWorkAnnexService; private readonly IConfiguration _Configuration; IMapper _IMapperOrder;
        public OrderController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkWCSService UnitOfWorkWCSService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkWCSService = UnitOfWorkWCSService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _IMapperOrder = MapperOrder.MapTo(); }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Order, EnumPermission.Actions.Action_WCS_Order_GetAll)]
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm] String SearchText, [FromForm] DtoFilterOrder Filters, [FromForm] int PageIndex, [FromForm] int PageSize, [FromForm] string SortItem, [FromForm] int SortType)
        {
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });

            DataTable DtFilterOrders = new DataTable();
            DtFilterOrders.Columns.Add("TF_FieldName");
            DtFilterOrders.Columns.Add("TF_Condition");

            if (SearchText == null) SearchText = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[0]["TF_FieldName"] = "SearchText";
            DtFilterOrders.Rows[0]["TF_Condition"] = SearchText;

            if (PageIndex < 0) PageIndex = 0;
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[1]["TF_FieldName"] = "PageIndex";
            DtFilterOrders.Rows[1]["TF_Condition"] = PageIndex;

            if (PageSize <= 0) PageSize = 50;
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[2]["TF_FieldName"] = "PageSize";
            DtFilterOrders.Rows[2]["TF_Condition"] = PageSize;

            if (SortType < 0) SortType = 1;
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[3]["TF_FieldName"] = "SortType";
            DtFilterOrders.Rows[3]["TF_Condition"] = SortType;

            if (SortItem == null) SortItem = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[4]["TF_FieldName"] = "SortItem";
            DtFilterOrders.Rows[4]["TF_Condition"] = SortItem;

            if (Filters.Filter_ProductGroup == null) Filters.Filter_ProductGroup = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[5]["TF_FieldName"] = "Product_Group";
            DtFilterOrders.Rows[5]["TF_Condition"] = Filters.Filter_ProductGroup;

            if (Filters.Filter_ProductType == null) Filters.Filter_ProductType = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[6]["TF_FieldName"] = "Product_Type";
            DtFilterOrders.Rows[6]["TF_Condition"] = Filters.Filter_ProductType;

            if (Filters.Filter_ProductDateStart == null) Filters.Filter_ProductDateStart = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[7]["TF_FieldName"] = "Product_DateRegisterStart";
            DtFilterOrders.Rows[7]["TF_Condition"] = Filters.Filter_ProductDateStart;

            if (Filters.Filter_ProductDateEnd == null) Filters.Filter_ProductDateEnd = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[8]["TF_FieldName"] = "Product_DateRegisterEnd";
            DtFilterOrders.Rows[8]["TF_Condition"] = Filters.Filter_ProductDateEnd;

            if (Filters.Filter_ProductIsUsed == null) Filters.Filter_ProductIsUsed = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[9]["TF_FieldName"] = "Product_IsUsed";
            DtFilterOrders.Rows[9]["TF_Condition"] = Filters.Filter_ProductIsUsed;

            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[10]["TF_FieldName"] = "MainRoleID";
            DtFilterOrders.Rows[10]["TF_Condition"] = (await _UnitOfWorkUCService._IRoleService.GetByWhere(R => R.Role_TagID == Convert.ToInt32(EnumPermission.Role.Role_WCS_User_WornCar))).Role_ID;


            var _Orders = await _UnitOfWorkWCSService._IOrderService.GetAll_SP(DtFilterOrders, 0);
            if (_Orders == null) return Ok(new { Message = MessageException.Messages.RequestNull, Status = MessageException.Status.Status400 });
            var TotalCount = _Orders.FirstOrDefault();
            int _Count = 0;
            if (TotalCount != null) _Count = Convert.ToInt32(TotalCount.Total_Count);
            return Ok(new { Orders = _Orders, Count = _Count, Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Order, EnumPermission.Actions.Action_WCS_Order_GetAllByUser)]
        [Route("GetAllByUser")]
        [HttpPost]
        public async Task<IActionResult> GetAllByUser([FromForm] String SearchText, [FromForm] DtoFilterOrder Filters, [FromForm] int PageIndex, [FromForm] int PageSize, [FromForm] string SortItem, [FromForm] int SortType)
        {
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });


            DataTable DtFilterOrders = new DataTable();
            DtFilterOrders.Columns.Add("TF_FieldName");
            DtFilterOrders.Columns.Add("TF_Condition");

            if (SearchText == null) SearchText = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[0]["TF_FieldName"] = "SearchText";
            DtFilterOrders.Rows[0]["TF_Condition"] = SearchText;

            if (PageIndex < 0) PageIndex = 0;
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[1]["TF_FieldName"] = "PageIndex";
            DtFilterOrders.Rows[1]["TF_Condition"] = PageIndex;

            if (PageSize <= 0) PageSize = 50;
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[2]["TF_FieldName"] = "PageSize";
            DtFilterOrders.Rows[2]["TF_Condition"] = PageSize;

            if (SortType < 0) SortType = 1;
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[3]["TF_FieldName"] = "SortType";
            DtFilterOrders.Rows[3]["TF_Condition"] = SortType;

            if (SortItem == null) SortItem = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[4]["TF_FieldName"] = "SortItem";
            DtFilterOrders.Rows[4]["TF_Condition"] = SortItem;

            if (Filters.Filter_ProductGroup == null) Filters.Filter_ProductGroup = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[5]["TF_FieldName"] = "Product_Group";
            DtFilterOrders.Rows[5]["TF_Condition"] = Filters.Filter_ProductGroup;

            if (Filters.Filter_ProductType == null) Filters.Filter_ProductType = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[6]["TF_FieldName"] = "Product_Type";
            DtFilterOrders.Rows[6]["TF_Condition"] = Filters.Filter_ProductType;

            if (Filters.Filter_ProductDateStart == null) Filters.Filter_ProductDateStart = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[7]["TF_FieldName"] = "Product_DateRegisterStart";
            DtFilterOrders.Rows[7]["TF_Condition"] = Filters.Filter_ProductDateStart;

            if (Filters.Filter_ProductDateEnd == null) Filters.Filter_ProductDateEnd = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[8]["TF_FieldName"] = "Product_DateRegisterEnd";
            DtFilterOrders.Rows[8]["TF_Condition"] = Filters.Filter_ProductDateEnd;

            if (Filters.Filter_ProductIsUsed == null) Filters.Filter_ProductIsUsed = "";
            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[9]["TF_FieldName"] = "Product_IsUsed";
            DtFilterOrders.Rows[9]["TF_Condition"] = Filters.Filter_ProductIsUsed;

            DtFilterOrders.Rows.Add();
            DtFilterOrders.Rows[10]["TF_FieldName"] = "MainRoleID";
            DtFilterOrders.Rows[10]["TF_Condition"] = (await _UnitOfWorkUCService._IRoleService.GetByWhere(R => R.Role_TagID == Convert.ToInt32(EnumPermission.Role.Role_WCS_User_WornCar))).Role_ID;


            var _Orders = await _UnitOfWorkWCSService._IOrderService.GetAll_SP(DtFilterOrders, _UserID);
            if (_Orders == null) return Ok(new { Message = MessageException.Messages.RequestNull, Status = MessageException.Status.Status400 });
            var TotalCount = _Orders.FirstOrDefault();
            int _Count = 0;
            if (TotalCount != null) _Count = Convert.ToInt32(TotalCount.Total_Count);
            return Ok(new { Orders = _Orders, Count = _Count, Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Order, EnumPermission.Actions.Action_WCS_Order_GetByID)]
        [Route("GetByOrderID")]
        [HttpPost]
        public async Task<IActionResult> GetByOrderID([FromForm] int OrdrID)
        {
            var _Order = await _UnitOfWorkWCSService._IOrderService.GetByOrder_SP(OrdrID);
            if (_Order == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { Order = _Order, Status = MessageException.Status.Status200 });
        }


        [AuthorizeCommon]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] DtoOrdersCustomer Orders)
        {
            if (Orders.OrdersProducts.Count == 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            //  var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == _UserID);
            _User.User_FirstName = Orders.FName;
            _User.User_LastName = Orders.LName;
            _User.User_Tell = Orders.Tell;
            _User.User_Province_ID = Orders.Province;
            _User.User_City_ID = Orders.City;
            _UnitOfWorkUCService._IUserService.Update(_User);
            await _UnitOfWorkUCService.SaveChange_DataBase_Async();
            var _NewListOrder = new List<Order>();
            foreach (var item in Orders.OrdersProducts)
            {
                _NewListOrder.Add(new Order
                {
                    Order_Count = item.PCount,
                    Order_DateRegister = DateTime.Now,
                    Order_Desc = "",
                    Order_UserID = _UserID,
                    Order_ProductID = item.PID,
                    Order_ResultComment = item.ResltComnt,
                    Order_PType = 0,
                    Order_OrderCode = 0,
                    Order_Price = (await _UnitOfWorkWCSService._IProductPriceService.GetLastProductPrice(item.PID)).ProductPrice_Price
                });
            }
            if (_NewListOrder != null)
            {
                await _UnitOfWorkWCSService._IOrderService.InsertRange(_NewListOrder);
                if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });

            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeCommon]
        [Route("Detail")]
        [HttpPost]
        public async Task<IActionResult> Detail([FromForm] int OrderId)
        {
            if (OrderId < 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var order = await _UnitOfWorkWCSService._IOrderService.GetByWhere(O => O.Order_ID == OrderId);
            if (order == null) return Ok(new { Message = MessageException.Messages.NullOrder.ToString(), Status = MessageException.Status.Status400 });
            var _order = _IMapperOrder.Map<Order, DtoOrder>(order);

            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _AnnexSettingCARPA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CARPA");
            var ProductImgAnnex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_ReferenceID == order.Order_ProductID & A.Annex_AnnexSettingID == _AnnexSettingCARPA.AnnexSetting_ID);
            var ProductImagePath = "";
            if (ProductImgAnnex != null)
            {
                ProductImagePath = ProductImgAnnex.Annex_Path + "/" + ProductImgAnnex.Annex_FileNamePhysicy + ProductImgAnnex.Annex_FileExtension;
            }
            return Ok(new { Order = _order, ProductImagePath = ProductImagePath, Status = MessageException.Status.Status200 });
        }

        [AuthorizeCommon]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] int OrderId, [FromForm] int OrdCount, [FromForm] string OrdDesc, [FromForm] Int16? OrdResultComment)
        {
            if (OrderId < 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var order = await _UnitOfWorkWCSService._IOrderService.GetByWhere(O => O.Order_ID == OrderId);
            if (order == null) return Ok(new { Message = MessageException.Messages.NullOrder.ToString(), Status = MessageException.Status.Status400 });
            order.Order_Count = OrdCount;
            order.Order_Desc = OrdDesc;
            order.Order_ResultComment = OrdResultComment;
            _UnitOfWorkWCSService._IOrderService.Update(order);
            if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }
    }
}
