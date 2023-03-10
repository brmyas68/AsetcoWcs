using Annex.ClassDTO.DTOs.Customs;
using Annex.InterfaceService.InterfacesBase;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;
using WCS.Common.Exceptions;
using WCS.Common.Mapping;
using WCS.InterfaceService.InterfacesBase;
using WCS.Common.Authorization;
using UC.ClassDomain.Domains;
using UC.InterfaceService.InterfacesBase;
using WCS.Common.Enum;

namespace WCS.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CivilController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private IUnitOfWorkWCSService _UnitOfWorkWCSService; private IUnitOfWorkAnnexService _UnitOfWorkAnnexService; private IConfiguration _Configuration; IMapper _IMapper;
        public CivilController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkWCSService UnitOfWorkWCSService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkWCSService = UnitOfWorkWCSService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _IMapper = MapperCivilContract.MapTo(); }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Civil, EnumPermission.Actions.Action_WCS_Civil_InquiryCivilRegister)]
        [Route("InquiryCivilRegister")]
        [HttpPost]
        public async Task<IActionResult> InquiryCivilRegister([FromForm] int? WMastrId, [FromForm] bool? ValidIdentNumber, [FromForm] bool? ValidnMobile, [FromForm] string ValidDesc)
        {
            var _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserId = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserId == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            if (WMastrId <= 0 || WMastrId == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _WornMaster = await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId);
            if (_WornMaster == null) return Ok(new { Message = MessageException.Messages.NullWornMaster.ToString(), Status = MessageException.Status.Status400 });
            if (_WornMaster.WornMaster_CivilContractID == null || _WornMaster.WornMaster_CivilContractID <= 0)
            {
                var _CivilContract = new CivilContract()
                {
                    CivilContract_InqValidationIdentifyNumber = ValidIdentNumber,
                    CivilContract_InqValidationMobile = ValidnMobile,
                    CivilContract_InqValidationDesc = ValidDesc,
                    CivilContract_InqValidationModifirID = _UserId,
                    CivilContract_InqValidationModifirDate = DateTime.Now,
                };

                await _UnitOfWorkWCSService._ICivilContractService.Insert(_CivilContract);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
                _WornMaster.WornMaster_CivilContractID = _CivilContract.CivilContract_ID;
                _UnitOfWorkWCSService._IWornMasterService.Update(_WornMaster);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            else
            {
                var _CivilContractness = await _UnitOfWorkWCSService._ICivilContractService.GetByWhere(C => C.CivilContract_ID == _WornMaster.WornMaster_CivilContractID);
                if (ValidIdentNumber != null) _CivilContractness.CivilContract_InqValidationIdentifyNumber = ValidIdentNumber.Value;
                if (ValidnMobile != null) _CivilContractness.CivilContract_InqValidationMobile = ValidnMobile.Value;
                if (ValidDesc != "") _CivilContractness.CivilContract_InqValidationDesc = ValidDesc;
                _CivilContractness.CivilContract_InqValidationModifirID = _UserId;
                _CivilContractness.CivilContract_InqValidationModifirDate = DateTime.Now;
                _UnitOfWorkWCSService._ICivilContractService.Update(_CivilContractness);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Civil, EnumPermission.Actions.Action_WCS_Civil_InquiryCivilRegister)]
        [Route("GetByInquiryCivilRegister")]
        [HttpPost]
        public async Task<IActionResult> GetByInquiryCivilRegister([FromForm] int WMastrId)
        {
            if (WMastrId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _CivilContractID = (await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId))?.WornMaster_CivilContractID;
            if (_CivilContractID == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _CivilContract = (await _UnitOfWorkWCSService._ICivilContractService.GetByWhere(B => B.CivilContract_ID == _CivilContractID.Value));

            var _Civil = new DtoCivilContract()
            {
                CvilCon_InqValIdentifyNumber = _CivilContract.CivilContract_InqValidationIdentifyNumber,
                CvilCon_InqValMobile = _CivilContract.CivilContract_InqValidationMobile,
                CvilCon_InqValDesc = _CivilContract.CivilContract_InqValidationDesc
            };

            return Ok(new { InquiryCivil = _Civil, Status = MessageException.Status.Status200 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Civil, EnumPermission.Actions.Action_WCS_Civil_InquirypoliceTraffic)]
        [Route("InquirypoliceTraffic")]
        [HttpPost]
        public async Task<IActionResult> InquirypoliceTraffic([FromForm] int? WMastrId, [FromForm] bool? BlkState, [FromForm] bool? ViolaState, [FromForm] Int64? ViolaAmount, [FromForm] string PolceDesc)
        {
            var _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserId = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserId == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            if (WMastrId <= 0 || WMastrId == null || ViolaAmount <= 0 || ViolaAmount == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _WornMaster = await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId);
            if (_WornMaster == null) return Ok(new { Message = MessageException.Messages.NullWornMaster.ToString(), Status = MessageException.Status.Status400 });
            if (_WornMaster.WornMaster_CivilContractID == null || _WornMaster.WornMaster_CivilContractID <= 0)
            {
                var _CivilContract = new CivilContract()
                {
                    CivilContract_InqBlockState = BlkState ,
                    CivilContract_InqViolationAmount = ViolaAmount ,
                    CivilContract_InqViolationState = ViolaState ,
                    CivilContract_InqPoliceDesc = PolceDesc,
                    CivilContract_InqPoliceModifirID = _UserId,
                    CivilContract_InqPoliceModifirDate = DateTime.Now,
                };

                await _UnitOfWorkWCSService._ICivilContractService.Insert(_CivilContract);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
                _WornMaster.WornMaster_CivilContractID = _CivilContract.CivilContract_ID;
                _UnitOfWorkWCSService._IWornMasterService.Update(_WornMaster);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            else
            {
                var _CivilContractness = await _UnitOfWorkWCSService._ICivilContractService.GetByWhere(C => C.CivilContract_ID == _WornMaster.WornMaster_CivilContractID);
                if (BlkState != null) _CivilContractness.CivilContract_InqBlockState = BlkState.Value;
                if (ViolaAmount != null) _CivilContractness.CivilContract_InqViolationAmount = ViolaAmount.Value;
                if (ViolaState != null) _CivilContractness.CivilContract_InqViolationState = ViolaState.Value;
                if (PolceDesc != "") _CivilContractness.CivilContract_InqPoliceDesc = PolceDesc;
                _CivilContractness.CivilContract_InqPoliceModifirID = _UserId;
                _CivilContractness.CivilContract_InqPoliceModifirDate = DateTime.Now;
                _UnitOfWorkWCSService._ICivilContractService.Update(_CivilContractness);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });

        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Civil, EnumPermission.Actions.Action_WCS_Civil_InquirypoliceTraffic)]
        [Route("GetByInquirypoliceTraffic")]
        [HttpPost]
        public async Task<IActionResult> GetByInquirypoliceTraffic([FromForm] int WMastrId)
        {
            if (WMastrId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _CivilContractID = (await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId))?.WornMaster_CivilContractID;
            if (_CivilContractID == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _CivilContract = (await _UnitOfWorkWCSService._ICivilContractService.GetByWhere(B => B.CivilContract_ID == _CivilContractID.Value));

            var InqViolAmont = _CivilContract.CivilContract_InqViolationAmount == null ? -1 : _CivilContract.CivilContract_InqViolationAmount.Value;

            var _Civil = new DtoCivilContract()
            {
                CvilCon_InqBlkState = _CivilContract.CivilContract_InqBlockState,
                CvilCon_InqViolAmont = InqViolAmont,
                CvilCon_InqViolState = _CivilContract.CivilContract_InqViolationState
            };

            return Ok(new { Inquirypolice = _Civil, Status = MessageException.Status.Status200 });

        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Civil, EnumPermission.Actions.Action_WCS_Civil_InquiryOffice)]
        [Route("InquiryOffice")]
        [HttpPost]
        public async Task<IActionResult> InquiryOffice([FromForm] int? WMastrId, [FromForm] bool IsOwnerCar, [FromForm] string IsOwnerCarDesc)
        {
            if (WMastrId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserId = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserId == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _WornMaster = await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId);
            if (_WornMaster == null) return Ok(new { Message = MessageException.Messages.NullWornMaster.ToString(), Status = MessageException.Status.Status400 });
            if (_WornMaster.WornMaster_CivilContractID == null || _WornMaster.WornMaster_CivilContractID <= 0)
            {
                var _CivilContract = new CivilContract()
                {
                    CivilContract_IsOwnerCar = IsOwnerCar,
                    CivilContract_IsOwnerCarDesc = IsOwnerCarDesc,
                };
                await _UnitOfWorkWCSService._ICivilContractService.Insert(_CivilContract);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
                _WornMaster.WornMaster_CivilContractID = _CivilContract.CivilContract_ID;
                _UnitOfWorkWCSService._IWornMasterService.Update(_WornMaster);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            else
            {
                var _CivilContractness = await _UnitOfWorkWCSService._ICivilContractService.GetByWhere(C => C.CivilContract_ID == _WornMaster.WornMaster_CivilContractID);
                _CivilContractness.CivilContract_IsOwnerCar = IsOwnerCar;
                _CivilContractness.CivilContract_IsOwnerCarDesc = IsOwnerCarDesc;
                _UnitOfWorkWCSService._ICivilContractService.Update(_CivilContractness);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Civil, EnumPermission.Actions.Action_WCS_Civil_InquiryOffice)]
        [Route("GetByInquiryOffice")]
        [HttpPost]
        public async Task<IActionResult> GetByInquiryOffice([FromForm] int WMastrId)
        {
            if (WMastrId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _CivilContractID = (await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId))?.WornMaster_CivilContractID;
            if (_CivilContractID == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _CivilContract = (await _UnitOfWorkWCSService._ICivilContractService.GetByWhere(B => B.CivilContract_ID == _CivilContractID.Value));
            var _Civil = new DtoCivilContract()
            {
                CvilCon_IsOwnerCar = _CivilContract.CivilContract_IsOwnerCar,
                CvilCon_IsOwnerCarDesc = _CivilContract.CivilContract_IsOwnerCarDesc,
            };

            return Ok(new { InquiryOffice = _Civil, Status = MessageException.Status.Status200 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Civil, EnumPermission.Actions.Action_WCS_Civil_LoadingContracts)]
        [Route("LoadingContracts")]
        [HttpPost]
        public async Task<IActionResult> LoadingContracts([FromForm] int? WMastrId, [FromForm] string DocName, [FromForm] DateTime? DocDate, [FromForm] string DocDesc, [FromForm] IFormFileCollection DocFiles)
        {
            if (WMastrId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserId = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserId == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _WornMaster = await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId);
            if (_WornMaster == null) return Ok(new { Message = MessageException.Messages.NullWornMaster.ToString(), Status = MessageException.Status.Status400 });
            if (_WornMaster.WornMaster_CivilContractID == null || _WornMaster.WornMaster_CivilContractID <= 0)
            {
                var _CivilContract = new CivilContract()
                {
                    CivilContract_DocumentName = DocName,
                    CivilContract_Date = DocDate,
                    CivilContract_Desc = DocDesc,
                    //   CivilContract_InqValidationModifirID = _UserId, 
                    //    CivilContract_InqValidationModifirDate = DateTime.Now,
                };
                await _UnitOfWorkWCSService._ICivilContractService.Insert(_CivilContract);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
                _WornMaster.WornMaster_CivilContractID = _CivilContract.CivilContract_ID;
                _UnitOfWorkWCSService._IWornMasterService.Update(_WornMaster);
                if (!(await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0)) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            }
            else
            {
                var _CivilContractness = await _UnitOfWorkWCSService._ICivilContractService.GetByWhere(C => C.CivilContract_ID == _WornMaster.WornMaster_CivilContractID);
                _CivilContractness.CivilContract_DocumentName = DocName;
                _CivilContractness.CivilContract_Date = DocDate;
                _CivilContractness.CivilContract_Desc = DocDesc;
                //   _CivilContractness.CivilContract_InqValidationModifirID = _UserId;
                //  _CivilContractness.CivilContract_InqValidationModifirDate = DateTime.Now;
                _UnitOfWorkWCSService._ICivilContractService.Update(_CivilContractness);
                if (!(await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0)) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            }

            if (DocFiles.Any())
            {
                var DtoListFiles = new List<DtoListFiles>();
                var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
                var _AnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CIVCA");
                foreach (var item in DocFiles)
                {
                    var DtoFiles = new DtoListFiles()
                    {
                        FileNameLogic = item.FileName,
                        ExtensionName = Path.GetExtension(item.FileName),
                        FileNamePhysic = Guid.NewGuid().ToString("N"),
                        FileStream = item.OpenReadStream(),
                        FileDesc = DocName,
                    };
                    DtoListFiles.Add(DtoFiles);
                }
                var _path = (_AnnexSetting.AnnexSetting_Path + "//" + _WornMaster.WornMaster_CivilContractID).ToString();
                var _stateDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_path.Replace("//", "/"));
                var AnnexList = await _UnitOfWorkAnnexService._IFtpService.UploadListFiles(DtoListFiles, _path, _WornMaster.WornMaster_CivilContractID.Value, _AnnexSetting.AnnexSetting_ID);
                await _UnitOfWorkAnnexService._IAnnexsService.InsertRange(AnnexList);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
            }
            return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Civil, EnumPermission.Actions.Action_WCS_Civil_LoadingContracts)]
        [Route("GetByLoadingContracts")]
        [HttpPost]
        public async Task<IActionResult> GetByLoadingContracts([FromForm] int WMastrId)
        {
            if (WMastrId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _CivilContractID = (await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(W => W.WornMaster_ID == WMastrId))?.WornMaster_CivilContractID;
            if (_CivilContractID == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _CivilContract = (await _UnitOfWorkWCSService._ICivilContractService.GetByWhere(B => B.CivilContract_ID == _CivilContractID.Value));

            var _Civil = new DtoCivilContract()
            {
                CvilCon_DocName = _CivilContract.CivilContract_DocumentName,
                CvilCon_InqDocModiDate = _CivilContract.CivilContract_Date,
                CvilCon_Desc = _CivilContract.CivilContract_Desc,
            };

            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _AnnexSettingCIVCA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CIVCA");
            var DtoListFiles = await _UnitOfWorkAnnexService._IAnnexsService.GetAllFiles(_CivilContractID.Value, _AnnexSettingCIVCA.AnnexSetting_ID);

            return Ok(new { LoadingContracts = _Civil, FilesLoadingContracts = DtoListFiles, Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Civil, EnumPermission.Actions.Action_WCS_Civil_DeleteFile)]
        [Route("DeleteFile")]
        [HttpPost]
        public async Task<IActionResult> DeleteFile([FromForm] int AnnexID)
        {
            if (AnnexID <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            // Delete File 
            var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_ID == AnnexID);
            if (_Annex == null) return Ok(new { Message = MessageException.Messages.NullAnnex.ToString(), Status = MessageException.Status.Status400 });
            var _Path = _Annex.Annex_Path + "/" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;
            await _UnitOfWorkAnnexService._IFtpService.DeleteImage(_Path);
            _UnitOfWorkAnnexService._IAnnexsService.Delete(_Annex);
            if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status200 });
        }

    }
}
