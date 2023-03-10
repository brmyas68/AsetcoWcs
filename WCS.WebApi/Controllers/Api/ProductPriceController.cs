using Annex.InterfaceService.InterfacesBase;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;
using WCS.Common.Authorization;
using WCS.Common.Mapping;
using WCS.Common.Enum;
using WCS.Common.Exceptions;
using WCS.InterfaceService.InterfacesBase;

namespace WCS.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPriceController : Controller
    {
        private IUnitOfWorkWCSService _UnitOfWorkWCSService;private readonly IConfiguration _Configuration; IMapper _IMapperProductPrice;
        public ProductPriceController(IUnitOfWorkWCSService UnitOfWorkWCSService, IConfiguration Configuration) { _UnitOfWorkWCSService = UnitOfWorkWCSService; _Configuration = Configuration; _IMapperProductPrice = MapperProductPrice.MapTo(); }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_ProductPrice, EnumPermission.Actions.Action_WCS_ProductPrice_GetAll)]
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm] int? ProductId, [FromForm] string Date)
        {

            var ProductsPrice = await _UnitOfWorkWCSService._IProductPriceService.GetAll_SP(ProductId, Date);
            if (ProductsPrice == null) return Ok(new { Message = MessageException.Messages.RequestNull, Status = MessageException.Status.Status400 });
            return Ok(new { ProductsPrice = ProductsPrice, Status = MessageException.Status.Status200 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_ProductPrice, EnumPermission.Actions.Action_WCS_ProductPrice_GetByID)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int ProdPricId)
        {
            if (ProdPricId <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var ProductsPrice = await _UnitOfWorkWCSService._IProductPriceService.GetPriceById_SP(ProdPricId);
            return Ok(new { ProductsPrice = ProductsPrice, Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_ProductPrice, EnumPermission.Actions.Action_WCS_ProductPrice_Insert)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoProductPrice DtoProdPric)
        {
            if (DtoProdPric == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _ProductsPrice = _IMapperProductPrice.Map<DtoProductPrice, ProductPrice>(DtoProdPric);
          //  _ProductsPrice.ProductPrice_Date = DateTime.Now;
            await _UnitOfWorkWCSService._IProductPriceService.Insert(_ProductsPrice);
            if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_ProductPrice, EnumPermission.Actions.Action_WCS_ProductPrice_Update)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoProductPrice DtoProdPric)
        {
            if (DtoProdPric == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _ProductsPrice = await _UnitOfWorkWCSService._IProductPriceService.GetByWhere(P => P.ProductPrice_ID == DtoProdPric.ProPric_ID);
            if (_ProductsPrice == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _ProductsPrice.ProductPrice_Price = DtoProdPric.ProPric_Price;
            if (DtoProdPric.ProPric_Date != null) _ProductsPrice.ProductPrice_Date = DtoProdPric.ProPric_Date.Value;
            _UnitOfWorkWCSService._IProductPriceService.Update(_ProductsPrice);
            if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_ProductPrice, EnumPermission.Actions.Action_WCS_ProductPrice_Delete)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int ProdPricId)
        {
            if (ProdPricId <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _ProductsPrice = await _UnitOfWorkWCSService._IProductPriceService.GetByWhere(P => P.ProductPrice_ID == ProdPricId);
            if (_ProductsPrice == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkWCSService._IProductPriceService.Delete(_ProductsPrice);
            if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }
    }
}
