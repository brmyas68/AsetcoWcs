using Annex.InterfaceService.InterfacesBase;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;
using WCS.Common.Authorization;
using WCS.Common.Exceptions;
using WCS.Common.Mapping;
using WCS.InterfaceService.InterfacesBase;
using Annex.ClassDTO.DTOs.Customs;
using WCS.ClassDTO.DTOs.Filters;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using UC.InterfaceService.InterfacesBase;
using WCS.Common.Enum;


namespace WCS.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductGroupController : ControllerBase
    {
        private IUnitOfWorkWCSService _UnitOfWorkWCSService; private IUnitOfWorkAnnexService _UnitOfWorkAnnexService; private readonly IConfiguration _Configuration; IMapper _IMapperProductGroup;
        public ProductGroupController(IUnitOfWorkWCSService UnitOfWorkWCSService, IConfiguration Configuration) { _UnitOfWorkWCSService = UnitOfWorkWCSService; _Configuration = Configuration; _IMapperProductGroup = MapperProductGroup.MapTo(); }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_ProductGroup, EnumPermission.Actions.Action_WCS_ProductGroup_GetAll)]
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm] int? TypeP)
        {
            var ProductGroups = new List<ProductGroup>();
            if (TypeP == null)
                ProductGroups = await _UnitOfWorkWCSService._IProductGroupService.GetAll();
            else
                ProductGroups = await _UnitOfWorkWCSService._IProductGroupService.GetAll(PG => PG.ProductGroup_Type == TypeP);
            if (ProductGroups == null) return Ok(new { Message = MessageException.Messages.RequestNull, Status = MessageException.Status.Status400 });
            var _ProductGroups = ProductGroups.Select(P => _IMapperProductGroup.Map<ProductGroup, DtoProductGroup>(P)).ToList();
            return Ok(new { ProductGroups = _ProductGroups, Status = MessageException.Status.Status200 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_ProductGroup, EnumPermission.Actions.Action_WCS_ProductGroup_GetByID)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int IdProdGrop)
        {
            if (IdProdGrop <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var ProductGroup = await _UnitOfWorkWCSService._IProductGroupService.GetByWhere(P => P.ProductGroup_ID == IdProdGrop);
            var _ProductGroup = _IMapperProductGroup.Map<ProductGroup, DtoProductGroup>(ProductGroup);
            return Ok(new { ProductGroup = _ProductGroup, Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_ProductGroup, EnumPermission.Actions.Action_WCS_ProductGroup_Insert)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoProductGroup DtoProdGrop)
        {
            if (DtoProdGrop == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _ProductGroup = _IMapperProductGroup.Map<DtoProductGroup, ProductGroup>(DtoProdGrop);
            await _UnitOfWorkWCSService._IProductGroupService.Insert(_ProductGroup);
            if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_ProductGroup, EnumPermission.Actions.Action_WCS_ProductGroup_Update)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoProductGroup DtoProdGrop)
        {
            if (DtoProdGrop == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _ProductGroup = await _UnitOfWorkWCSService._IProductGroupService.GetByWhere(P => P.ProductGroup_ID == DtoProdGrop.PG_ID);
            if (_ProductGroup == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _ProductGroup.ProductGroup_Name = DtoProdGrop.PG_Name;
            _ProductGroup.ProductGroup_Type = DtoProdGrop.PG_Type;
            _UnitOfWorkWCSService._IProductGroupService.Update(_ProductGroup);
            if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_ProductGroup, EnumPermission.Actions.Action_WCS_ProductGroup_Delete)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int ProdGId)
        {
            if (ProdGId <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _ProductGroup = await _UnitOfWorkWCSService._IProductGroupService.GetByWhere(P => P.ProductGroup_ID == ProdGId);
            if (_ProductGroup == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkWCSService._IProductGroupService.Delete(_ProductGroup);
            if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }
    }
}
