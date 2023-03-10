using Annex.InterfaceService.InterfacesBase;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UC.InterfaceService.InterfacesBase;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;
using WCS.Common.Authorization;
using WCS.Common.Enum;
using WCS.Common.Exceptions;
using WCS.Common.Mapping;
using WCS.InterfaceService.InterfacesBase;

namespace WCS.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private IUnitOfWorkWCSService _UnitOfWorkWCSService; private IUnitOfWorkAnnexService _UnitOfWorkAnnexService; private readonly IConfiguration _Configuration; private IMapper _IMapper;
        public MessageController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkWCSService UnitOfWorkWCSService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkWCSService = UnitOfWorkWCSService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _IMapper = MapperMessage.MapTo(); }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Message, EnumPermission.Actions.Action_WCS_Message_GetAll)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var _Messages = await _UnitOfWorkWCSService._IMessageService.GetAll();
            if (_Messages == null) return Ok(new { Message = MessageException.Messages.NullMessages.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { Messages = _Messages, Status = MessageException.Status.Status200 });
        }

        [Route("Send")]
        [HttpPost]
        public async Task<IActionResult> Send([FromForm] DtoMessage _DtoMsg, [FromForm] string SecurityCode, [FromForm] string TokenSecurityCode)
        {
            if (_DtoMsg == null) return Ok(new { Message = MessageException.Messages.NullMessages.ToString(), Status = MessageException.Status.Status400 });
            var KeySecurity = _Configuration["Jwt:KeySecurityCode"].ToString();
            string _TokenSecurityCode = KeySecurity + "Mehraman" + SecurityCode.ToString() + "AsetCo";
            string _HashTokenSecurityCode = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(_TokenSecurityCode);
            if (!(_UnitOfWorkUCService._ISecurityCodeService.IValidSecurityCode(TokenSecurityCode, _HashTokenSecurityCode))) return Ok(new { Message = MessageException.Messages.NoMatchSecurityCode, Status = MessageException.Status.Status400 });
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            _DtoMsg.Msg_DateSend = DateTime.Now;
            _DtoMsg.Msg_TenatID = TenantID;
            var _Message = _IMapper.Map<DtoMessage, Message>(_DtoMsg);
            await _UnitOfWorkWCSService._IMessageService.Insert(_Message);
            if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Message, EnumPermission.Actions.Action_WCS_Message_Delete)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int MsgId)
        {
            if (MsgId <= 0) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            var _Message = await _UnitOfWorkWCSService._IMessageService.GetByWhere(M => M.Message_ID == MsgId);
            _UnitOfWorkWCSService._IMessageService.Delete(_Message);
            if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }
    }
}
