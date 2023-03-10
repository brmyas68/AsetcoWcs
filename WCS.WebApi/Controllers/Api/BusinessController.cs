using Annex.InterfaceService.InterfacesBase;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using UC.ClassDTO.DTOs;
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
    public class BusinessController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private IUnitOfWorkWCSService _UnitOfWorkWCSService; private IUnitOfWorkAnnexService _UnitOfWorkAnnexService; private readonly IConfiguration _Configuration; IMapper _IMapper;
        public BusinessController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkWCSService UnitOfWorkWCSService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkWCSService = UnitOfWorkWCSService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _IMapper = MapperBusiness.MapTo(); }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Business, EnumPermission.Actions.Action_WCS_Business_DeterminSuggestPrice)]
        [Route("DeterminSuggestPrice")]
        [HttpPost]
        public async Task<IActionResult> DeterminSuggestPrice([FromForm] int? WMastrId, [FromForm] Int64 MinPrice, [FromForm] Int64 MaxPrice, [FromForm] string PriceDesc)
        {
            if (WMastrId <= 0 || WMastrId == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _WornMaster = await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId);
            if (_WornMaster == null) return Ok(new { Message = MessageException.Messages.NullWornMaster.ToString(), Status = MessageException.Status.Status400 });
            if (_WornMaster.WornMaster_BusinessID == null || _WornMaster.WornMaster_BusinessID <= 0)
            {
                var _Business = new Business()
                {
                    Business_MinPrice = MinPrice,
                    Business_MaxPrice = MaxPrice,
                    Business_PriceDesc = PriceDesc
                };
                await _UnitOfWorkWCSService._IBusinessService.Insert(_Business);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
                _WornMaster.WornMaster_BusinessID = _Business.Business_ID;
                _UnitOfWorkWCSService._IWornMasterService.Update(_WornMaster);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            else
            {
                var _Business = await _UnitOfWorkWCSService._IBusinessService.GetByWhere(B => B.Business_ID == _WornMaster.WornMaster_BusinessID);
                _Business.Business_MinPrice = MinPrice;
                _Business.Business_MaxPrice = MaxPrice;
                _Business.Business_PriceDesc = PriceDesc;
                _UnitOfWorkWCSService._IBusinessService.Update(_Business);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            var _Owners = await _UnitOfWorkWCSService._IOwnersService.GetByWhere(O => O.Owners_ID == _WornMaster.WornMaster_OwnerID);
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == _Owners.Owners_UserID);
            if (_User != null)
            {
                var _Sms = await _UnitOfWorkUCService._ISmsService.Send(_User.User_Mobile, MinPrice.ToString(), MaxPrice.ToString(), "DeterminPrice");
                if (_Sms == null) return Ok(new { Message = MessageException.Messages.ErrorNullSms.ToString(), Status = MessageException.Status.Status400 });
            }
            return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Business, EnumPermission.Actions.Action_WCS_Business_AgreedPrice)]
        [Route("AgreedPrice")]
        [HttpPost]
        public async Task<IActionResult> AgreedPrice([FromForm] int? WMastrId, [FromForm] int InvestorID, [FromForm] Int64 AgrAmount, [FromForm] Int64 PreAgrAmount, [FromForm] string AgrDesc)
        {
            if (WMastrId <= 0 || WMastrId == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _WornMaster = await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId);
            if (_WornMaster == null) return Ok(new { Message = MessageException.Messages.NullWornMaster.ToString(), Status = MessageException.Status.Status400 });
            if (_WornMaster.WornMaster_BusinessID == null || _WornMaster.WornMaster_BusinessID <= 0)
            {
                var _Business = new Business()
                {
                    Business_AgreementAmount = AgrAmount,
                    Business_PreAgreementAmount = PreAgrAmount,
                    Business_AgreementDesc = AgrDesc,
                    Business_Investor = InvestorID,
                };
                await _UnitOfWorkWCSService._IBusinessService.Insert(_Business);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
                _WornMaster.WornMaster_BusinessID = _Business.Business_ID;
                _UnitOfWorkWCSService._IWornMasterService.Update(_WornMaster);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            else
            {
                var _Business = await _UnitOfWorkWCSService._IBusinessService.GetByWhere(B => B.Business_ID == _WornMaster.WornMaster_BusinessID);
                _Business.Business_AgreementAmount = AgrAmount;
                _Business.Business_PreAgreementAmount = PreAgrAmount;
                _Business.Business_AgreementDesc = AgrDesc;
                _Business.Business_Investor = InvestorID;
                _UnitOfWorkWCSService._IBusinessService.Update(_Business);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Business, EnumPermission.Actions.Action_WCS_Business_DeterminParking)]
        [Route("DeterminParking")]
        [HttpPost]
        public async Task<IActionResult> DeterminParking([FromForm] int? WMastrId, [FromForm] int ParkID, [FromForm] DateTime? ParkDate, [FromForm] string ParkDesc)
        {
            if (WMastrId <= 0 || WMastrId == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _WornMaster = await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId);
            if (_WornMaster == null) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            if (_WornMaster.WornMaster_BusinessID == null || _WornMaster.WornMaster_BusinessID <= 0)
            {
                var _Business = new Business()
                {
                    Business_ParkingWornCenterID = ParkID,
                    Business_ParkingDate = ParkDate,
                    Business_ParkingDesc = ParkDesc,


                };
                await _UnitOfWorkWCSService._IBusinessService.Insert(_Business);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
                _WornMaster.WornMaster_BusinessID = _Business.Business_ID;
                _UnitOfWorkWCSService._IWornMasterService.Update(_WornMaster);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            else
            {
                var _Business = await _UnitOfWorkWCSService._IBusinessService.GetByWhere(B => B.Business_ID == _WornMaster.WornMaster_BusinessID);
                _Business.Business_ParkingWornCenterID = ParkID;
                _Business.Business_ParkingDate = ParkDate;
                _Business.Business_ParkingDesc = ParkDesc;
                _UnitOfWorkWCSService._IBusinessService.Update(_Business);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Business, EnumPermission.Actions.Action_WCS_Business_DeterminParking)]
        [Route("GetByDeterminParking")]
        [HttpPost]
        public async Task<IActionResult> GetByDeterminParking([FromForm] int WMastrId)
        {
            if (WMastrId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _BusinessID = (await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId))?.WornMaster_BusinessID;
            if (_BusinessID == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _Business = (await _UnitOfWorkWCSService._IBusinessService.GetByWhere(B => B.Business_ID == _BusinessID.Value));

            var ParkWCntrID = _Business.Business_ParkingWornCenterID == null ? -1 : _Business.Business_ParkingWornCenterID.Value;
            // var ParkDate = _Business.Business_ParkingDate == null ? DateTime.ParseExact("0/0/0000 0:00:00 PM", "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-us")) : _Business.Business_ParkingDate.Value;

            var _Busi = new DtoBusiness()
            {
                Busi_ParkWCntrID = ParkWCntrID,
                Busi_ParkDate = _Business.Business_ParkingDate,
                Busi_ParkDesc = _Business.Business_ParkingDesc
            };

            return Ok(new { DeterminPark = _Busi, Status = MessageException.Status.Status200 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Business, EnumPermission.Actions.Action_WCS_Business_DeterminWornCenter)]
        [Route("DeterminWornCenter")]
        [HttpPost]
        public async Task<IActionResult> DeterminWornCenter([FromForm] int? WMastrId, [FromForm] int SpltCntrID, [FromForm] DateTime? SpltDate, [FromForm] string SpltDesc)
        {
            if (WMastrId <= 0 || WMastrId == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _WornMaster = await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId);
            if (_WornMaster == null) return Ok(new { Message = MessageException.Messages.NullWornMaster.ToString(), Status = MessageException.Status.Status400 });
            if (_WornMaster.WornMaster_BusinessID == null || _WornMaster.WornMaster_BusinessID <= 0)
            {
                var _Business = new Business()
                {
                    Business_SplitWornCenterID = SpltCntrID,
                    Business_SplitDate = SpltDate,
                    Business_SplitDesc = SpltDesc
                };
                await _UnitOfWorkWCSService._IBusinessService.Insert(_Business);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
                _WornMaster.WornMaster_BusinessID = _Business.Business_ID;
                _UnitOfWorkWCSService._IWornMasterService.Update(_WornMaster);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            else
            {
                var _Business = await _UnitOfWorkWCSService._IBusinessService.GetByWhere(B => B.Business_ID == _WornMaster.WornMaster_BusinessID);
                _Business.Business_SplitWornCenterID = SpltCntrID;
                _Business.Business_SplitDate = SpltDate;
                _Business.Business_SplitDesc = SpltDesc;
                _UnitOfWorkWCSService._IBusinessService.Update(_Business);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Business, EnumPermission.Actions.Action_WCS_Business_DeterminWornCenter)]
        [Route("GetByDeterminWornCenter")]
        [HttpPost]
        public async Task<IActionResult> GetByDeterminWornCenter([FromForm] int WMastrId)
        {
            if (WMastrId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _BusinessID = (await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId))?.WornMaster_BusinessID;
            if (_BusinessID == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _Business = (await _UnitOfWorkWCSService._IBusinessService.GetByWhere(B => B.Business_ID == _BusinessID.Value));

            var SplitWCntrID = _Business.Business_SplitWornCenterID == null ? -1 : _Business.Business_SplitWornCenterID.Value;


            var _Busi = new DtoBusiness()
            {
                Busi_SplitWCntrID = SplitWCntrID,
                Busi_SplitDate = _Business.Business_SplitDate.Value,
                Busi_SplitDesc = _Business.Business_SplitDesc
            };

            return Ok(new { DeterminSplit = _Busi, Status = MessageException.Status.Status200 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Business, EnumPermission.Actions.Action_WCS_Business_StateTrackingCode)]
        [Route("StateTrackingCode")]
        [HttpPost]
        public async Task<IActionResult> StateTrackingCode([FromForm] int? WMastrId, [FromForm] Int16 WMasState, [FromForm] string WMasStateDesc)
        {
            if (WMastrId <= 0 || WMastrId == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _WornMaster = await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId);
            if (_WornMaster == null) return Ok(new { Message = MessageException.Messages.NullWornMaster.ToString(), Status = MessageException.Status.Status400 });
            _WornMaster.WornMaster_State = WMasState;
            _WornMaster.WornMaster_StateDesc = WMasStateDesc;
            _UnitOfWorkWCSService._IWornMasterService.Update(_WornMaster);
            if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Business, EnumPermission.Actions.Action_WCS_Business_GetAllInvestor)]
        [Route("GetAllInvestor")]
        [HttpGet]
        public async Task<IActionResult> GetAllInvestor()
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _Users = await _UnitOfWorkUCService._IUserService.GetAllUsersByRoleTagName_SP(EnumPermission.Role.Role_WCS_User_Investor.ToString(), TenantID);
            if (_Users == null) return Ok(new { Message = MessageException.Messages.NullUserInvestor.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { Users = _Users, Status = MessageException.Status.Status200 });
        }
    }
}
