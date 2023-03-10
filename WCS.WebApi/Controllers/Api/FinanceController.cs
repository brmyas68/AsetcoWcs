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
using UC.InterfaceService.InterfacesBase;
using WCS.Common.Enum;

namespace WCS.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private IUnitOfWorkWCSService _UnitOfWorkWCSService; private IUnitOfWorkAnnexService _UnitOfWorkAnnexService; private readonly IConfiguration _Configuration; IMapper _IMapper;
        public FinanceController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkWCSService UnitOfWorkWCSService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkWCSService = UnitOfWorkWCSService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _IMapper = MapperFinance.MapTo(); }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Finance, EnumPermission.Actions.Action_WCS_Finance_GetAll)]
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm] int WMastrId)
        {
            if (WMastrId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _Finances = await _UnitOfWorkWCSService._IFinanceService.GetAllFinanceByWMasterID(WMastrId);
            if (_Finances == null) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            var _Finance = _Finances.Select(F => _IMapper.Map<Finance, DtoFinance>(F)).ToList();
            return Ok(new { Finance = _Finance, Status = MessageException.Status.Status200 }); ;
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Finance, EnumPermission.Actions.Action_WCS_Finance_Insert)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] int WMastrId, [FromForm] DtoFinance DtoFinance, [FromForm] IFormFileCollection FileFinance)
        {
            var _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserId = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;

            var Finance = new Finance()
            {
                Finance_WornMasterID = WMastrId,
                Finance_Amount = DtoFinance.Fin_Amont,
                Finance_PaymentType = DtoFinance.Fin_PayType,
                Finance_RegisterDate = DtoFinance.Fin_RegDate,
                Finance_Desc = DtoFinance.Fin_Desc,
                Finance_ModifirID = _UserId,
                Finance_ModifirDate = DtoFinance.Fin_RegDate //DateTime.Now
            };
            await _UnitOfWorkWCSService._IFinanceService.Insert(Finance);
            if (!(await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0)) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });

            if (FileFinance.Any())
            {
                var DtoListFiles = new List<DtoListFiles>();
                var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
                var _AnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "FINCA");
                foreach (var item in FileFinance)
                {
                    var DtoFiles = new DtoListFiles()
                    {
                        FileNameLogic = item.FileName,
                        ExtensionName = Path.GetExtension(item.FileName),
                        FileNamePhysic = Guid.NewGuid().ToString("N"),
                        FileStream = item.OpenReadStream(),
                        FileDesc = Finance.Finance_Desc,
                    };
                    DtoListFiles.Add(DtoFiles);
                }
                var _path = (_AnnexSetting.AnnexSetting_Path + "//" + Finance.Finance_ID).ToString();
                var _stateDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_path.Replace("//", "/"));
                var AnnexList = await _UnitOfWorkAnnexService._IFtpService.UploadListFiles(DtoListFiles, _path, Finance.Finance_ID, _AnnexSetting.AnnexSetting_ID);
                await _UnitOfWorkAnnexService._IAnnexsService.InsertRange(AnnexList);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
            }

            return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Finance, EnumPermission.Actions.Action_WCS_Finance_Update)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoFinance DtoFinance)
        {
            var _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserId = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (DtoFinance == null) return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
            var _Finance = await _UnitOfWorkWCSService._IFinanceService.GetByWhere(F => F.Finance_ID == DtoFinance.Fin_ID);
            if (_Finance == null) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            _Finance.Finance_Amount = DtoFinance.Fin_Amont;
            _Finance.Finance_Desc = DtoFinance.Fin_Desc;
            _Finance.Finance_PaymentType = DtoFinance.Fin_PayType;
            _Finance.Finance_ModifirID = DtoFinance.Fin_ModiID;
            _Finance.Finance_ModifirDate = DateTime.Now;
            _Finance.Finance_RegisterDate = DtoFinance.Fin_RegDate;
            _UnitOfWorkWCSService._IFinanceService.Update(_Finance);
            if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Finance, EnumPermission.Actions.Action_WCS_Finance_Delete)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int FincId)
        {
            if (FincId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _AnnexSettingFINCA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "FINCA");
            var _Finance = await _UnitOfWorkWCSService._IFinanceService.GetByWhere(F => F.Finance_ID == FincId);
            var State = false;
            if (_Finance != null)
            {
                _UnitOfWorkWCSService._IFinanceService.Delete(_Finance);
                State = (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0);
            }
            await _UnitOfWorkAnnexService._IFtpService.DeleteDirectoryFiles(_AnnexSettingFINCA.AnnexSetting_Path + "//" + FincId + "//");
            var ListAnnex = await _UnitOfWorkAnnexService._IAnnexsService.GetAll(A => A.Annex_ReferenceID == FincId & A.Annex_AnnexSettingID == _AnnexSettingFINCA.AnnexSetting_ID);
            if (ListAnnex != null)
            {
                _UnitOfWorkAnnexService._IAnnexsService.DeleteRange(ListAnnex);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
            }
            if (State) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Finance, EnumPermission.Actions.Action_WCS_Finance_UploadFile)]
        [Route("UploadFile")]
        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] int FincId, [FromForm] IFormFileCollection ProductFiles)
        {
            if (FincId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            // upload Files  
            if (ProductFiles.Any())
            {
                var DtoListFiles = new List<DtoListFiles>();
                var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
                var _AnnexSettingFINCA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "FINCA");
                foreach (var item in ProductFiles)
                {
                    var DtoFiles = new DtoListFiles()
                    {
                        FileNameLogic = item.FileName,
                        ExtensionName = Path.GetExtension(item.FileName),
                        FileNamePhysic = Guid.NewGuid().ToString("N"),
                        FileStream = item.OpenReadStream(),
                        FileDesc = FincId.ToString(),

                    };
                    DtoListFiles.Add(DtoFiles);
                }
                var _path = (_AnnexSettingFINCA.AnnexSetting_Path + "//" + FincId).ToString();
                var _stateDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_path.Replace("//", "/"));
                var AnnexList = await _UnitOfWorkAnnexService._IFtpService.UploadListFiles(DtoListFiles, _path, FincId, _AnnexSettingFINCA.AnnexSetting_ID);
                await _UnitOfWorkAnnexService._IAnnexsService.InsertRange(AnnexList);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
            }

            return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Finance, EnumPermission.Actions.Action_WCS_Finance_DeleteFile)]
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

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Finance, EnumPermission.Actions.Action_WCS_Finance_GetAllFiles)]
        [Route("GetAllFiles")]
        [HttpPost]
        public async Task<IActionResult> GetAllFiles([FromForm] int FincId)
        {
            if (FincId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _AnnexSettingFINCA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "FINCA");
            var DtoListFiles = _UnitOfWorkAnnexService._IAnnexsService.GetAllFiles(FincId, _AnnexSettingFINCA.AnnexSetting_ID);
            if (DtoListFiles == null) return Ok(new { Message = MessageException.Messages.NullFilesProduct.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { FilesProduct = DtoListFiles, Status = MessageException.Status.Status200 });
        }
    }
}
