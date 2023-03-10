using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

using WCS.Common.Enum;
using UC.InterfaceService.InterfacesBase;
using WCS.ClassDomain.Domains;
using WCS.Common.Exceptions;
using WCS.InterfaceService.InterfacesBase;

namespace WCS.WebApi.Controllers.Api.Authentication
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private IUnitOfWorkWCSService _UnitOfWorkWCSService; private readonly IConfiguration _Configuration;
        public AuthenticationController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkWCSService UnitOfWorkWCSService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkWCSService = UnitOfWorkWCSService; _Configuration = Configuration; }


        [AllowAnonymous]
        [Route("SendActiveCode")]
        [HttpPost]
        public async Task<IActionResult> SendActiveCode([FromForm] string ReceptorMobile)
        {
            if (ReceptorMobile == "") return Ok(new { Message = MessageException.Messages.ErrorNoMobile.ToString(), Status = MessageException.Status.Status400 });
            if (!(await _UnitOfWorkUCService._ISmsService.Delete(ReceptorMobile))) return Ok(new { Message = MessageException.Messages.ErrorNoDeleteSms.ToString(), Status = MessageException.Status.Status400 });
            var _ActiveCode = _UnitOfWorkUCService._ISmsService.GenerateActiveCode();
            var _TextMessage = "homacall"; var SenderMobile = "";
            if (_ActiveCode <= 0) return Ok(new { Message = MessageException.Messages.ErrorActiveCode.ToString(), Status = MessageException.Status.Status400 });
            var _Sms = await _UnitOfWorkUCService._ISmsService.Send(SenderMobile, ReceptorMobile, _ActiveCode.ToString(), _TextMessage);
            if (_Sms == null) return Ok(new { Message = MessageException.Messages.ErrorNullSms.ToString(), Status = MessageException.Status.Status400 });
            if (_Sms.Sms_Status == 400) return Ok(new { Message = MessageException.Messages.ErrorSendSms.ToString(), Status = MessageException.Status.Status400 });
            await _UnitOfWorkUCService._ISmsService.Insert(_Sms);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AllowAnonymous]
        [Route("IsValidActiveCode")]
        [HttpPost]
        public async Task<IActionResult> IsValidActiveCode([FromForm] string ReceptorMobile, [FromForm] string ActiveCode)
        {
            if (ReceptorMobile == "" && ActiveCode == "" && ActiveCode.Length > 5 && ReceptorMobile.Length > 11) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            if (!(await _UnitOfWorkUCService._ISmsService.FindByActiveCode(ReceptorMobile, ActiveCode))) return Ok(new { Message = MessageException.Messages.NotFoundActiveCode.ToString(), Status = MessageException.Status.Status400 });
            await _UnitOfWorkUCService._ISmsService.Delete(ReceptorMobile);
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_Mobile == ReceptorMobile);
            var _UserID = -1;
            if (_User == null)
            {
                var _TnatID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
                User _user = new User
                {
                    User_TenantID = _TnatID,
                    User_Address = "",
                    User_Province_ID = -1,
                    User_City_ID = -1,
                    User_IdentifyNumber = "",
                    User_FirstName = "",
                    User_LastName = "",
                    User_Gender = 1,
                    User_HashPassword = "",
                    User_IsActive = true,
                    User_IsBlock = false,
                    User_Email = "",
                    User_Mobile = ReceptorMobile,
                    User_UserName = ReceptorMobile,
                    User_DateRegister = DateTime.Now,
                };
                await _UnitOfWorkUCService._IUserService.Insert(_user);
                if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0)
                {
                    _UserID = (await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_Mobile == ReceptorMobile && U.User_TenantID == _TnatID)).User_ID;
                    UserRole _UserRole = new UserRole
                    {
                        Role_ID = (await _UnitOfWorkUCService._IRoleService.GetByWhere(R => R.Role_TagID == Convert.ToInt32(EnumPermission.Role.Role_WCS_User_WornCar))).Role_ID,
                        User_ID = _UserID
                    };
                    await _UnitOfWorkUCService._IUserRoleService.Insert(_UserRole);
                    await _UnitOfWorkUCService.SaveChange_DataBase_Async();
                }
            }
            else
            {
                _UserID = _User.User_ID;
                int Rol_ID = (await _UnitOfWorkUCService._IRoleService.GetByWhere(R => R.Role_TagID == Convert.ToInt32(EnumPermission.Role.Role_WCS_User_WornCar))).Role_ID;
                if (!await _UnitOfWorkUCService._IUserRoleService.CheckUserRole(_UserID, Rol_ID)) return Ok(new
                {
                    Message = MessageException.Messages.NotAccess.ToString(),
                    Status = MessageException.Status.Status400
                });
            }
            var _Owner = new Owners()
            {
                Owners_UserID = _UserID,
                Owners_ShabaNumber = "",
                Owners_Desc = "",
            };
            await _UnitOfWorkWCSService._IOwnersService.Insert(_Owner);
            if (!(await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0)) return Ok(new { Message = MessageException.Messages.ErrorOwner.ToString(), Status = MessageException.Status.Status400 });
            DtoSettingToken _DtoSettingToken = new DtoSettingToken();
            _DtoSettingToken.Signing_Key = _Configuration["Jwt:Key"].ToString();
            _DtoSettingToken.Issuer = _Configuration["Jwt:Issuer"].ToString();
            _DtoSettingToken.Audience = _Configuration["Jwt:Audience"].ToString();
            _DtoSettingToken.Subject = _Configuration["Jwt:Subject"].ToString();
            _DtoSettingToken.Expir_Minutes = Convert.ToDouble(SettingToken.Expir.Minutes);
            _DtoSettingToken.DateTime_UtcNow = DateTime.UtcNow;
            _DtoSettingToken.DateTime_Now = DateTime.Now;
            _DtoSettingToken.Guid = Guid.NewGuid();
            var _Token = await _UnitOfWorkUCService._IAuthenticationService.CreateToken(_UserID, _DtoSettingToken);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.ErrorToken.ToString(), Status = MessageException.Status.Status400 });
            Token token = new Token();
            token.Token_UserID = _UserID;
            token.Token_HashCode = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(_Token);
            token.Token_DateCreate = DateTime.Now;
            token.Token_DateExpire = DateTime.Now.AddHours(Convert.ToDouble(SettingToken.Expir.AddHours)); //1 Hours add
            token.Token_DateLastAccessTime = DateTime.Now.AddHours(Convert.ToDouble(SettingToken.Expir.ExpirDateLastHours));
            token.Token_IsActive = true;
            await _UnitOfWorkUCService._ITokenService.Insert(token);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Token = _Token, Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }


        [AllowAnonymous]
        [Route("GenrateSecurityCode")]
        [HttpGet]
        public IActionResult GenrateSecurityCode()
        {
            int _SecurityCode = _UnitOfWorkUCService._ISecurityCodeService.GenarateSecurityCode();
            var KeySecurity = _Configuration["Jwt:KeySecurityCode"].ToString();
            string _TokenSecurityCode = KeySecurity + "Mehraman" + _SecurityCode.ToString() + "AsetCo";
            string _HashTokenSecurityCode = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(_TokenSecurityCode);
            var _Bitmap64 = _UnitOfWorkUCService._ISecurityCodeService.CreateImageSecurityCode(_SecurityCode);
            // await Task.CompletedTask;
            return Ok(new { BitmapSecurityCode = _Bitmap64, TokenSecurityCode = _HashTokenSecurityCode });
        }

    }
}
