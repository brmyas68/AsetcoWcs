using Annex.InterfaceService.InterfacesBase;
using Annex.Service.ServiceBase;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Filters;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;
using WCS.ClassDTO.DTOs.Customs;
using WCS.ClassDTO.DTOs.Filters;
using WCS.Common.Authorization;
using WCS.Common.Exceptions;
using WCS.Common.Mapping;
using WCS.InterfaceService.InterfacesBase;
using Annex.ClassDTO.DTOs.Customs;
using Annex.Service.ExternalServices;

using UC.Interface.Interfaces;
using Annex.ClassDomain.Domains;
using UC.InterfaceService.InterfacesBase;
using WCS.Common.Enum;

namespace WCS.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class WornMasterController : ControllerBase
    {

        private IUnitOfWorkUCService _UnitOfWorkUCService; private IUnitOfWorkWCSService _UnitOfWorkWCSService; private IUnitOfWorkAnnexService _UnitOfWorkAnnexService; private readonly IConfiguration _Configuration; private IMapper _MapperWornCar;
        public WornMasterController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkWCSService UnitOfWorkWCSService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkWCSService = UnitOfWorkWCSService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _MapperWornCar = MapperWornCars.MapTo(); }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_WornMaster, EnumPermission.Actions.Action_WCS_WornMaster_GetAll)]
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm] String? SearchText, [FromForm] DtoFilterWornMaster? Filters, [FromForm] int PageIndex, [FromForm] int PageSize, [FromForm] string? SortItem, [FromForm] int SortType)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);

            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });

            //   var _UserID = 52;

            DataTable DtFilterWornMaster = new DataTable();
            DtFilterWornMaster.Columns.Add("TF_FieldName");
            DtFilterWornMaster.Columns.Add("TF_Condition");

            if (SearchText == null) SearchText = "";
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[0]["TF_FieldName"] = "SearchText";
            DtFilterWornMaster.Rows[0]["TF_Condition"] = SearchText;

            if (PageIndex < 0) PageIndex = 0;
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[1]["TF_FieldName"] = "PageIndex";
            DtFilterWornMaster.Rows[1]["TF_Condition"] = PageIndex;

            if (PageSize <= 0) PageSize = 50;
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[2]["TF_FieldName"] = "PageSize";
            DtFilterWornMaster.Rows[2]["TF_Condition"] = PageSize;

            if (SortType < 0) SortType = 1;
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[3]["TF_FieldName"] = "SortType";
            DtFilterWornMaster.Rows[3]["TF_Condition"] = SortType;

            if (SortItem == null) SortItem = "";
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[4]["TF_FieldName"] = "SortItem";
            DtFilterWornMaster.Rows[4]["TF_Condition"] = SortItem;

            if (Filters.Filter_State == null) Filters.Filter_State = "";
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[5]["TF_FieldName"] = "WornMaster_State";
            DtFilterWornMaster.Rows[5]["TF_Condition"] = Filters.Filter_State;

            if (Filters.Filter_Inquiry == null) Filters.Filter_Inquiry = "";
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[6]["TF_FieldName"] = "Inquiry";
            DtFilterWornMaster.Rows[6]["TF_Condition"] = Filters.Filter_Inquiry;

            if (Filters.Filter_CivilContract == null) Filters.Filter_CivilContract = "";
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[7]["TF_FieldName"] = "CivilContract_State";
            DtFilterWornMaster.Rows[7]["TF_Condition"] = Filters.Filter_CivilContract;

            if (Filters.Filter_Agent == null) Filters.Filter_Agent = "";
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[8]["TF_FieldName"] = "WornMaster_AgentID";
            DtFilterWornMaster.Rows[8]["TF_Condition"] = Filters.Filter_Agent;

            if (Filters.Filter_Parking == null) Filters.Filter_Parking = "";
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[9]["TF_FieldName"] = "WornCenter_ParkingID";
            DtFilterWornMaster.Rows[9]["TF_Condition"] = Filters.Filter_Parking;

            if (Filters.Filter_SplitWorn == null) Filters.Filter_SplitWorn = "";
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[10]["TF_FieldName"] = "WornCenter_SplitWornID";
            DtFilterWornMaster.Rows[10]["TF_Condition"] = Filters.Filter_SplitWorn;

            if (Filters.Filter_DateRegister == null) Filters.Filter_DateRegister = "";
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[11]["TF_FieldName"] = "WornMaster_RegisterDate";
            DtFilterWornMaster.Rows[11]["TF_Condition"] = Filters.Filter_DateRegister;

            if (Filters.Filter_Province == null) Filters.Filter_Province = "";
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[12]["TF_FieldName"] = "WornCars_ProvinceID";
            DtFilterWornMaster.Rows[12]["TF_Condition"] = Filters.Filter_Province;

            if (Filters.Filter_City == null) Filters.Filter_City = "";
            DtFilterWornMaster.Rows.Add();
            DtFilterWornMaster.Rows[13]["TF_FieldName"] = "WornCars_CityID";
            DtFilterWornMaster.Rows[13]["TF_Condition"] = Filters.Filter_City;

            var _WMatsers = await _UnitOfWorkWCSService._IWornMasterService.GetAll_SP(DtFilterWornMaster, _UserID);
            if (_WMatsers == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var TotalCount = _WMatsers.FirstOrDefault();
            int _Count = 0;
            if (TotalCount != null) _Count = Convert.ToInt32(TotalCount.Total_Count);
            return Ok(new { WMatsers = _WMatsers, Count = _Count, Status = MessageException.Status.Status200 });

        }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_WornMaster, EnumPermission.Actions.Action_WCS_WornMaster_GetByID)]
        [Route("GetByWMasterID")]
        [HttpPost]
        public async Task<IActionResult> GetByWMasterID([FromForm] int WMasterID)
        {
            var _WMatser = await _UnitOfWorkWCSService._IWornMasterService.GetByWMaster_SP(WMasterID);
            if (_WMatser == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { WMatser = _WMatser, Status = MessageException.Status.Status200 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_WornMaster, EnumPermission.Actions.Action_WCS_WornMaster_InquiryPrice)]
        [Route("InquiryPrice")]
        [HttpPost]
        public async Task<IActionResult> InquiryPrice([FromForm] DtoWornCars DtoWornCars, [FromForm] IFormFileCollection FileInquiry)
        {
            var _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _WornCars = new WornCars()
            {
                //WornCars_BrandID = DtoWornCars.WCars_BrdID,
                WornCars_ModelID = DtoWornCars.WCars_ModID,
                WornCars_BuildYear = DtoWornCars.WCars_BldYear,
                WornCars_PelakType = DtoWornCars.WCars_PlkType,
                WornCars_UserType = DtoWornCars.WCars_UsrType,
                WornCars_State = DtoWornCars.WCars_State,
                WornCars_Desc = DtoWornCars.WCars_Desc,
                // WornCars_Weight = 0,
                //  WornCars_DocumentType = -1,
            };
            await _UnitOfWorkWCSService._IWornCarsService.Insert(_WornCars);
            if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0)
            {
                var _WornMaster = new WornMaster()
                {
                    WornMaster_OwnerID = (await _UnitOfWorkWCSService._IOwnersService.GetByWhere(o => o.Owners_UserID == UserID)).Owners_ID,
                    WornMaster_WornCarID = _WornCars.WornCars_ID,
                    WornMaster_State = 0,
                    WornMaster_RegisterDate = DateTime.Now,
                    WornMaster_WornCarState = false,
                };
                await _UnitOfWorkWCSService._IWornMasterService.Insert(_WornMaster);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();

                if (FileInquiry.Any())
                {
                    var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
                    var DtoListFiles = new List<DtoListFiles>();
                    var _AnnexSettingCARIA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CARIA");
                    foreach (var item in FileInquiry)
                    {
                        var DtoFiles = new DtoListFiles()
                        {
                            FileNameLogic = item.FileName,
                            ExtensionName = Path.GetExtension(item.FileName),
                            FileNamePhysic = Guid.NewGuid().ToString("N"),
                            FileStream = item.OpenReadStream(),
                        };
                        DtoListFiles.Add(DtoFiles);
                    }
                    var _path = (_AnnexSettingCARIA.AnnexSetting_Path + "//" + _WornCars.WornCars_ID).ToString();
                    var _stateDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_path.Replace("//", "/"));
                    var AnnexList = await _UnitOfWorkAnnexService._IFtpService.UploadListFiles(DtoListFiles, _path, _WornCars.WornCars_ID, _AnnexSettingCARIA.AnnexSetting_ID);
                    await _UnitOfWorkAnnexService._IAnnexsService.InsertRange(AnnexList);
                    await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
                }

                return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }

            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_WornMaster, EnumPermission.Actions.Action_WCS_WornMaster_Insert)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoOwners_ DtoOwn, [FromForm] DtoWornCars DtoWcar,
        [FromForm] IFormFileCollection FileDocsCar, [FromForm] IFormFileCollection FileCardsCar, [FromForm] IFormFileCollection FileImagesCar,
        [FromForm] IFormFileCollection FileOtherDocs, [FromForm] DtoDescCar_ DtoDesc, [FromForm] int AgentID, [FromForm] string SecurityCode, [FromForm] string TokenSecurityCode)
        {
            string _Token = string.Empty; var _WornMasterID = -1;
            var KeySecurity = _Configuration["Jwt:KeySecurityCode"].ToString();
            string _TokenSecurityCode = KeySecurity + "Mehraman" + SecurityCode.ToString() + "AsetCo";
            string _HashTokenSecurityCode = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(_TokenSecurityCode);
            if (!(_UnitOfWorkUCService._ISecurityCodeService.IValidSecurityCode(TokenSecurityCode, _HashTokenSecurityCode))) return Ok(new { Message = MessageException.Messages.NoMatchSecurityCode.ToString(), Status = MessageException.Status.Status400 });

            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == _UserID); // Get User By UserId
            if (DtoOwn == null) return Ok(new { Message = MessageException.Messages.EmptyOwner.ToString(), Status = MessageException.Status.Status400 });
            // Fill User  
            _User.User_City_ID = DtoOwn.Own_cityId;
            _User.User_Province_ID = DtoOwn.Own_ProvncId;
            // _User.Usr_Mobile = DtoOwn.Own_Mobile;
            _User.User_IdentifyNumber = DtoOwn.Own_IdenNumber;
            _User.User_FirstName = DtoOwn.Own_FName;
            _User.User_LastName = DtoOwn.Own_LName;
            //_User.tell = DtoOwn.Own_Tell;
            // update  user
            _UnitOfWorkUCService._IUserService.Update(_User);
            if (!(await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0)) return Ok(new { Message = MessageException.Messages.NoUpdateUser.ToString(), Status = MessageException.Status.Status400 });
            //  get owner by user id
            var _Owner = await _UnitOfWorkWCSService._IOwnersService.GetByWhere(O => O.Owners_UserID == _UserID);
            _Owner.Owners_Desc = DtoOwn.Own_Desc;
            _Owner.Owners_ShabaNumber = DtoOwn.Own_ShbaNum;
            _Owner.Owners_Tell = DtoOwn.Own_Tell;
            // update owner 
            _UnitOfWorkWCSService._IOwnersService.Update(_Owner);
            if (!(await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0)) return Ok(new { Message = MessageException.Messages.NoUpdateOwner.ToString(), Status = MessageException.Status.Status400 });
            // update or new worncar
            if (DtoWcar != null)
            {
                // get  worncar by worncar id
                if (DtoWcar.WCars_ID > 0)
                {
                    var _WornCar = await _UnitOfWorkWCSService._IWornCarsService.GetByWhere(W => W.WornCars_ID == DtoWcar.WCars_ID);
                    _WornCar.WornCars_Weight = DtoWcar.WCars_Weight;
                    _WornCar.WornCars_DocumentType = DtoWcar.WCars_DocType;
                    _WornCar.WornCars_Desc = DtoWcar.WCars_Desc;
                    _UnitOfWorkWCSService._IWornCarsService.Update(_WornCar);
                    if (!(await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0)) return Ok(new { Message = MessageException.Messages.ErrorWornCar.ToString(), Status = MessageException.Status.Status400 });
                    _WornMasterID = (await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(M => M.WornMaster_WornCarID == DtoWcar.WCars_ID && M.WornMaster_OwnerID == _Owner.Owners_ID)).WornMaster_ID;
                }
                else
                {
                    var _WornCars = new WornCars()
                    {

                        //  WornCars_BrandID = DtoWcar.WCars_BrdID,
                        WornCars_ModelID = DtoWcar.WCars_ModID,
                        WornCars_BuildYear = DtoWcar.WCars_BldYear,
                        WornCars_PelakType = DtoWcar.WCars_PlkType,
                        WornCars_UserType = DtoWcar.WCars_UsrType,
                        WornCars_State = DtoWcar.WCars_State,
                        WornCars_Desc = DtoWcar.WCars_Desc,
                        WornCars_Weight = DtoWcar.WCars_Weight,
                        WornCars_DocumentType = DtoWcar.WCars_DocType,
                    };
                    await _UnitOfWorkWCSService._IWornCarsService.Insert(_WornCars);
                    if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0)
                    {
                        var _WornMasterNew = new WornMaster()
                        {
                            WornMaster_OwnerID = _Owner.Owners_ID,
                            WornMaster_WornCarID = _WornCars.WornCars_ID,
                            WornMaster_State = 0,
                            WornMaster_RegisterDate = DateTime.Now,
                            WornMaster_StateDesc = "",
                            WornMaster_WornCarState = false,
                        };
                        await _UnitOfWorkWCSService._IWornMasterService.Insert(_WornMasterNew);
                        if (!(await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0)) return Ok(new { Message = MessageException.Messages.ErrorWornCar.ToString(), Status = MessageException.Status.Status400 });
                        _WornMasterID = _WornMasterNew.WornMaster_ID;

                    }
                }
            }
            var _WornMaster = await _UnitOfWorkWCSService._IWornMasterService.GetByWhere(M => M.WornMaster_ID == _WornMasterID);
            if (_WornMaster == null) return Ok(new { Message = MessageException.Messages.NotFoundWornMaster.ToString(), Status = MessageException.Status.Status400 });
            if (AgentID > 0) // update agentid in  wornmaster
            {
                _WornMaster.WornMaster_AgentID = AgentID;
            }
            // GenerateTrackingCode  and update wornmaster
            var _TrackingCode = _UnitOfWorkWCSService._IWornMasterService.GenerateTrackingCode();
            _WornMaster.WornMaster_TrackingCode = _TrackingCode;
            _WornMaster.WornMaster_State = 0;
            _WornMaster.WornMaster_StateDesc = "در جریان کار قرار گیرد";
            _WornMaster.WornMaster_WornCarState = true;
            _UnitOfWorkWCSService._IWornMasterService.Update(_WornMaster);
            if (!(await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0)) return Ok(new { Message = MessageException.Messages.NoUpdateWornMaster.ToString(), Status = MessageException.Status.Status400 });

            // upload Files  

            if (FileDocsCar.Any())
            {
                var DtoListFiles = new List<DtoListFiles>();
                var _AnnexSettingCARDA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_User.User_TenantID, "CARDA");
                foreach (var item in FileDocsCar)
                {
                    var DtoFiles = new DtoListFiles()
                    {
                        FileNameLogic = item.FileName,
                        ExtensionName = Path.GetExtension(item.FileName),
                        FileNamePhysic = Guid.NewGuid().ToString("N"),
                        FileStream = item.OpenReadStream(),
                        FileDesc = DtoDesc.WCars_DescDocsCar,
                    };
                    DtoListFiles.Add(DtoFiles);
                }
                var _path = (_AnnexSettingCARDA.AnnexSetting_Path + "//" + _WornMaster.WornMaster_WornCarID).ToString();
                var _stateDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_path.Replace("//", "/"));
                var AnnexList = await _UnitOfWorkAnnexService._IFtpService.UploadListFiles(DtoListFiles, _path, _WornMaster.WornMaster_WornCarID, _AnnexSettingCARDA.AnnexSetting_ID);
                await _UnitOfWorkAnnexService._IAnnexsService.InsertRange(AnnexList);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
            }

            if (FileCardsCar.Any())
            {
                var DtoListFiles = new List<DtoListFiles>();
                var _AnnexSettingCARCA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_User.User_TenantID, "CARCA");
                foreach (var item in FileCardsCar)
                {
                    var DtoFiles = new DtoListFiles()
                    {
                        FileNameLogic = item.FileName,
                        ExtensionName = Path.GetExtension(item.FileName),
                        FileNamePhysic = Guid.NewGuid().ToString("N"),
                        FileStream = item.OpenReadStream(),
                        FileDesc = DtoDesc.WCars_DescCardsCar,
                    };
                    DtoListFiles.Add(DtoFiles);
                }
                var _path = (_AnnexSettingCARCA.AnnexSetting_Path + "//" + _WornMaster.WornMaster_WornCarID).ToString();
                var _stateDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_path.Replace("//", "/"));
                var AnnexList = await _UnitOfWorkAnnexService._IFtpService.UploadListFiles(DtoListFiles, _path, _WornMaster.WornMaster_WornCarID, _AnnexSettingCARCA.AnnexSetting_ID);
                await _UnitOfWorkAnnexService._IAnnexsService.InsertRange(AnnexList);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
            }

            if (FileImagesCar.Any())
            {
                var DtoListFiles = new List<DtoListFiles>();
                var _AnnexSettingCARIA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_User.User_TenantID, "CARIA");
                foreach (var item in FileImagesCar)
                {
                    var DtoFiles = new DtoListFiles()
                    {
                        FileNameLogic = item.FileName,
                        ExtensionName = Path.GetExtension(item.FileName),
                        FileNamePhysic = Guid.NewGuid().ToString("N"),
                        FileStream = item.OpenReadStream(),
                        FileDesc = DtoDesc.WCars_DescImagesCar,
                    };
                    DtoListFiles.Add(DtoFiles);
                }
                var _path = (_AnnexSettingCARIA.AnnexSetting_Path + "//" + _WornMaster.WornMaster_WornCarID).ToString();
                var _stateDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_path.Replace("//", "/"));
                var AnnexList = await _UnitOfWorkAnnexService._IFtpService.UploadListFiles(DtoListFiles, _path, _WornMaster.WornMaster_WornCarID, _AnnexSettingCARIA.AnnexSetting_ID);
                await _UnitOfWorkAnnexService._IAnnexsService.InsertRange(AnnexList);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
            }

            if (FileOtherDocs.Any())
            {
                var DtoListFiles = new List<DtoListFiles>();
                var _AnnexSettingCAROA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_User.User_TenantID, "CAROA");
                foreach (var item in FileOtherDocs)
                {
                    var DtoFiles = new DtoListFiles()
                    {
                        FileNameLogic = item.FileName,
                        ExtensionName = Path.GetExtension(item.FileName),
                        FileNamePhysic = Guid.NewGuid().ToString("N"),
                        FileStream = item.OpenReadStream(),
                        FileDesc = DtoDesc.WCars_DescOtherDocs,
                    };
                    DtoListFiles.Add(DtoFiles);
                }
                var _path = (_AnnexSettingCAROA.AnnexSetting_Path + "//" + _WornMaster.WornMaster_WornCarID).ToString();
                var _stateDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_path.Replace("//", "/"));
                var AnnexList = await _UnitOfWorkAnnexService._IFtpService.UploadListFiles(DtoListFiles, _path, _WornMaster.WornMaster_WornCarID, _AnnexSettingCAROA.AnnexSetting_ID);
                await _UnitOfWorkAnnexService._IAnnexsService.InsertRange(AnnexList);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
            }



            var _TextMessage = "homacall"; var SenderMobile = "";
            if (_TrackingCode == "") return Ok(new { Message = MessageException.Messages.NullTrackingCode.ToString(), Status = MessageException.Status.Status400 });
            // sms TrackingCode
            var _Sms = await _UnitOfWorkUCService._ISmsService.Send(SenderMobile, DtoOwn.Own_Mobile, _TrackingCode.ToString(), _TextMessage);
            if (_Sms == null) return Ok(new { Message = MessageException.Messages.ErrorNullSms.ToString(), Status = MessageException.Status.Status400 });
            // Sucess
            return Ok(new { TrackingCode = _TrackingCode.ToString(), Status = MessageException.Status.Status200 });
        }

        [AuthorizeCommon]
        [Route("GetWornCarById")]
        [HttpPost]
        public async Task<IActionResult> GetWornCarById([FromForm] int WCarID)
        {
            if (WCarID < 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var WornCar = await _UnitOfWorkWCSService._IWornCarsService.GetByWhere(W => W.WornCars_ID == WCarID);
            if (WornCar == null) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            var _WornCar = _MapperWornCar.Map<WornCars, DtoWornCars>(WornCar);
            var _WorncarBrandID = (await _UnitOfWorkUCService._IModelCarService.GetByWhere(M => M.ModelCar_ID == _WornCar.WCars_ModID)).ModelCar_BrandCarID;
            return Ok(new { WornCar = _WornCar, BrandID = _WorncarBrandID, Status = MessageException.Status.Status200 });
        }

        [AuthorizeCommon]
        [Route("GetAllWornCarByUserId")]
        [HttpGet]
        public async Task<IActionResult> GetAllWornCarByUserId()
        {
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _WornCars = await _UnitOfWorkWCSService._IWornMasterService.GetAllWornCars(_UserID);
            if (_WornCars == null) return Ok(new { Message = MessageException.Messages.NullWornCars.ToString(), Status = MessageException.Status.Status400.ToString() });
            return Ok(new { WornCars = _WornCars, Status = MessageException.Status.Status200 });
        }

        [AuthorizeCommon]
        [Route("GetAllUserAgents")]
        [HttpGet]
        public async Task<IActionResult> GetAllUserAgents()
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _UserAgents = await _UnitOfWorkUCService._IUserService.GetAllUsersByRoleTagName_SP(EnumPermission.Role.Role_WCS_Agent_WornCar.ToString(), TenantID);
            if (_UserAgents == null) return Ok(new { Message = MessageException.Messages.NullUserAgents.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { UserAgents = _UserAgents, Status = MessageException.Status.Status200 });
        }

        [AuthorizeCommon]
        [Route("GetAllFiles")]
        [HttpPost]
        public async Task<IActionResult> GetAllFiles([FromForm] int WornCarId)
        {
            if (WornCarId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _AnnexSettingCARDA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CARDA");
            var _AnnexSettingCARCA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CARCA");
            var _AnnexSettingCARIA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CARIA");
            var _AnnexSettingCAROA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CAROA");
            var DtoListFilesCARDA = await _UnitOfWorkAnnexService._IAnnexsService.GetAllFiles(WornCarId, _AnnexSettingCARDA.AnnexSetting_ID);
            var DtoListFilesCARCA = await _UnitOfWorkAnnexService._IAnnexsService.GetAllFiles(WornCarId, _AnnexSettingCARCA.AnnexSetting_ID);
            var DtoListFilesCARIA = await _UnitOfWorkAnnexService._IAnnexsService.GetAllFiles(WornCarId, _AnnexSettingCARIA.AnnexSetting_ID);
            var DtoListFilesCAROA = await _UnitOfWorkAnnexService._IAnnexsService.GetAllFiles(WornCarId, _AnnexSettingCAROA.AnnexSetting_ID);
            return Ok(new
            {
                FilesDocuments = DtoListFilesCARDA,
                FilesCards = DtoListFilesCARCA,
                FilesImages = DtoListFilesCARIA,
                FilesOthers = DtoListFilesCAROA,
                Status = MessageException.Status.Status200
            });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_WornMaster, EnumPermission.Actions.Action_WCS_WornMaster_UploadImage)]
        [Route("UploadImage")]
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile IFileWorn, [FromForm] int WCID, [FromForm] int Type)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _KeyWord = "";
            if (Type == 1) _KeyWord = "CARIA";
            if (Type == 2) _KeyWord = "CARCA";
            if (Type == 3) _KeyWord = "CARDA";
            if (Type == 4) _KeyWord = "CAROA";
            var _AnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, _KeyWord);
            var _Path = "";

            string _PhysicyNameFile = Guid.NewGuid().ToString("N");
            _Path = _AnnexSetting.AnnexSetting_Path + "//" + WCID.ToString();
            var _boolDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_Path);
            var _boolUpload = await _UnitOfWorkAnnexService._IFtpService.UploadImage(IFileWorn.OpenReadStream(), _PhysicyNameFile, Path.GetExtension(IFileWorn.FileName), _Path);
            if (!_boolUpload) return Ok(new { Message = MessageException.Messages.NoUploadUser.ToString(), Status = MessageException.Status.Status400.ToString() });
            Annexs Annex_ = new Annexs()
            {
                Annex_Path = _Path.Replace("//", "/"),
                Annex_FileNamePhysicy = _PhysicyNameFile,
                Annex_FileNameLogic = IFileWorn.FileName,
                Annex_ReferenceID = WCID,
                Annex_CreatedDate = DateTime.Now,
                Annex_Description = "",
                Annex_ReferenceFolderName = WCID.ToString() + ":" + IFileWorn.FileName.ToString(),
                Annex_AnnexSettingID = _AnnexSetting.AnnexSetting_ID,
                Annex_FileExtension = Path.GetExtension(IFileWorn.FileName),
                Annex_IsDefault = true
            };
            await _UnitOfWorkAnnexService._IAnnexsService.Insert(Annex_);
            if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200.ToString() });
            return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400.ToString() });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_WornMaster, EnumPermission.Actions.Action_WCS_WornMaster_DeleteFile)]
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

