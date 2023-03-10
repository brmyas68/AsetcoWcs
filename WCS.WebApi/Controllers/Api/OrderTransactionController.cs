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
using Kavenegar.Core.Json;

namespace WCS.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTransactionController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private IUnitOfWorkWCSService _UnitOfWorkWCSService; private IUnitOfWorkAnnexService _UnitOfWorkAnnexService; private readonly IConfiguration _Configuration; IMapper _IMapperOrderTransaction;
        public OrderTransactionController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkWCSService UnitOfWorkWCSService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkWCSService = UnitOfWorkWCSService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _IMapperOrderTransaction = MapperOrderTransaction.MapTo(); }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_OrderTransaction, EnumPermission.Actions.Action_WCS_OrderTransaction_GetAll)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var _OrderTransaction = await _UnitOfWorkWCSService._IOrderTransactionService.GetAll();
            if (_OrderTransaction == null) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            var OrderTransaction = _OrderTransaction.Select(O => _IMapperOrderTransaction.Map<OrderTransaction, DtoOrderTransaction>(O)).ToList();
            return Ok(new { OrderTransaction = OrderTransaction, Status = MessageException.Status.Status200 });

        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_OrderTransaction, EnumPermission.Actions.Action_WCS_OrderTransaction_GetAllByUser)]
        [Route("GetAllByUser")]
        [HttpGet]
        public async Task<IActionResult> GetAllByUser()
        {
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });

            var _OrderTransaction = await _UnitOfWorkWCSService._IOrderTransactionService.GetAll(O=>O.OrderTransaction_UserID == _UserID);
            if (_OrderTransaction == null) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            var OrderTransaction = _OrderTransaction.Select(O => _IMapperOrderTransaction.Map<OrderTransaction, DtoOrderTransaction>(O)).ToList();
            return Ok(new { OrderTransaction = OrderTransaction, Status = MessageException.Status.Status200 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_OrderTransaction, EnumPermission.Actions.Action_WCS_OrderTransaction_GetByID)]
        [Route("GetByOrderTransID")]
        [HttpPost]
        public async Task<IActionResult> GetByOrderTransacID([FromForm] int OrderTransID)
        {
            var _OrderTrans = await _UnitOfWorkWCSService._IOrderTransactionService.GetByWhere(O=>O.OrderTransaction_ID == OrderTransID);
            if (_OrderTrans == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var OrderTrans = _IMapperOrderTransaction.Map<OrderTransaction, DtoOrderTransaction>(_OrderTrans);
            return Ok(new { OrderTransaction = OrderTrans, Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_OrderTransaction, EnumPermission.Actions.Action_WCS_OrderTransaction_GetByOrderCode)]
        [Route("GetByOrderCode")]
        [HttpPost]
        public async Task<IActionResult> GetByOrderCode([FromForm] int OrderCode)
        {
            var _OrderTrans = await _UnitOfWorkWCSService._IOrderTransactionService.GetByWhere(O => O.OrderTransaction_OrderCode == OrderCode);
            if (_OrderTrans == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var OrderTrans = _IMapperOrderTransaction.Map<OrderTransaction, DtoOrderTransaction>(_OrderTrans);
            return Ok(new { OrderTransaction = OrderTrans, Status = MessageException.Status.Status200 });
        }

    }
}
