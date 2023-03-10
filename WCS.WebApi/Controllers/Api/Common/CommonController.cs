using Annex.ClassDomain.Domains;
using Annex.InterfaceService.InterfacesBase;
using Annex.Service.ServiceBase;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.Common.Authorization;
using UC.Common.Mapping;
using UC.InterfaceService.InterfacesBase;
using UC.Service.ServiceBase;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;
using WCS.ClassDTO.DTOs.Filters;
using WCS.Common.Exceptions;
using WCS.Common.Mapping;
using WCS.InterfaceService.InterfacesBase;

namespace WCS.WebApi.Controllers.Api.Common
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private IUnitOfWorkWCSService _UnitOfWorkWCSService; private IUnitOfWorkAnnexService _UnitOfWorkAnnexService; private readonly IConfiguration _Configuration; private IMapper _MapperWornCenter; private IMapper _MapperBrand; private IMapper _MapperModel; private IMapper _MapperOwner; private IMapper _MapperCity; private IMapper _MapperProvince; IMapper _IMapperProductGroup;
        public CommonController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkWCSService UnitOfWorkWCSService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkWCSService = UnitOfWorkWCSService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _IMapperProductGroup = MapperProductGroup.MapTo(); _MapperBrand = MapperBrandCar.MapTo(); _MapperModel = MapperModelCar.MapTo(); _MapperOwner = MapperOwners.MapTo(); _MapperCity = MapperCity.MapTo(); _MapperProvince = MapperProvince.MapTo(); _MapperWornCenter = MapperWornCenter.MapTo(); }


        [AuthorizeCommon]
        [Route("GetAllProvince")]
        [HttpGet]
        public async Task<IActionResult> GetAllProvince()
        {
            var _Provinces = await _UnitOfWorkUCService._IProvinceService.GetAll();
            if (_Provinces == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var Provinces = _Provinces.Select(P => _MapperProvince.Map<Province, DtoProvince>(P)).ToList();
            return Ok(new { Provinces = Provinces, Status = MessageException.Status.Status200 });
        }


        [AuthorizeCommon]
        [Route("GetAllCity")]
        [HttpPost]
        public async Task<IActionResult> GetAllCity([FromForm] int PrviceID)
        {
            if (PrviceID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _Citys = await _UnitOfWorkUCService._ICityService.GetAll_By_ProvinceID(PrviceID);
            if (_Citys == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var Citys = _Citys.Select(C => _MapperCity.Map<City, DtoCity>(C)).ToList();
            return Ok(new { City = Citys, Status = MessageException.Status.Status200 });
        }


        [AuthorizeCommon]
        [Route("GetAllBrand")]
        [HttpGet]
        public async Task<IActionResult> GetAllBrand()
        {
            var Brands = await _UnitOfWorkUCService._IBrandCarService.GetAll(B=> B.BrandCar_Type != VehicleType.Motorcycle);
            if (Brands == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _Brands = Brands.Select(B => _MapperBrand.Map<BrandCar, DtoBrandCar>(B)).ToList();
            return Ok(new { Brands = _Brands, Status = MessageException.Status.Status200 });
        }


        [AuthorizeCommon]
        [Route("GetAllModel")]
        [HttpPost]
        public async Task<IActionResult> GetAllModel([FromForm] int BrandID)
        {
            if (BrandID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var Models = await _UnitOfWorkUCService._IModelCarService.GetAll(M=>M.ModelCar_BrandCarID == BrandID);
            if (Models == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _Models = Models.Select(M => _MapperModel.Map<ModelCar, DtoModelCar>(M)).ToList();
            return Ok(new { Models = _Models, Status = MessageException.Status.Status200 });
        }


        [AuthorizeCommon]
        [Route("GetAllParking")]
        [HttpPost]
        public async Task<IActionResult> GetAllParking([FromForm] int WCenterType)
        {
            var WornCenters = await _UnitOfWorkWCSService._IWornCenterService.GetAllWornCenterByType(WCenterType);
            if (WornCenters == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _WornCenters = WornCenters.Select(P => _MapperWornCenter.Map<WornCenter, DtoWornCenter>(P)).ToList();
            return Ok(new { WornCenters = _WornCenters, Status = MessageException.Status.Status200 });
        }


        [AuthorizeCommon]
        [Route("UpdateUserProfile")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserProfile([FromForm] string FName, [FromForm] string LName, [FromForm] string ShebaNum, [FromForm] string IdentNum, [FromForm] string Tell, [FromForm] int ProvID, [FromForm] int CityId)
        {
            if (FName == "" && ShebaNum == "" && IdentNum == "" && LName == "") return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _User = (await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == _UserID));
            if (_User == null) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _Owner = await _UnitOfWorkWCSService._IOwnersService.GetByWhere(O => O.Owners_UserID == _UserID);
            _User.User_IdentifyNumber = IdentNum;
            _User.User_FirstName = FName;
            _User.User_LastName = LName;
            _User.User_Province_ID = ProvID;
            _User.User_City_ID = CityId;
            _Owner.Owners_ShabaNumber = ShebaNum;
            _Owner.Owners_Tell = Tell;
            _UnitOfWorkUCService._IUserService.Update(_User);
            _UnitOfWorkWCSService._IOwnersService.Update(_Owner);
            await _UnitOfWorkUCService.SaveChange_DataBase_Async();
            await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
        }


        [AuthorizeCommon]
        [Route("GetInfoUser")]
        [HttpGet]
        public async Task<IActionResult> GetInfoUser()
        {
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _User = await _UnitOfWorkUCService._IUserService.GetUser_By_ID_SP(_UserID);
            var _Owner = await _UnitOfWorkWCSService._IOwnersService.GetByWhere(O => O.Owners_UserID == _UserID);
            var _DtoOwner = _MapperOwner.Map<Owners, DtoOwners>(_Owner);
            var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_User.Usr_TnatID, "USRA");
            var _AnnexSeting_ID = _DtoAnnexSetting.AnnexSetting_ID;
            var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_AnnexSettingID == _AnnexSeting_ID && A.Annex_ReferenceID == _User.Usr_ID);
            var _Path = "";
            if (_Annex != null) _Path = _Annex.Annex_Path + "/" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;
            _User.Usr_Img = _Path;
            return Ok(new { User = _User, Owner = _DtoOwner, Status = MessageException.Status.Status200 });
        }


        [AuthorizeCommon]
        [Route("UploadImage")]
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile IFileUser)
        {
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == _UserID);
            if (_User == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_User.User_TenantID, "USRA");
            var _AnnexSeting_ID = _DtoAnnexSetting.AnnexSetting_ID;
            var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_AnnexSettingID == _AnnexSeting_ID && A.Annex_ReferenceID == _User.User_ID && A.Annex_IsDefault == true);
            var _Path = "";
            if (_Annex != null)
            {
                string _PhysicyNameFile = Guid.NewGuid().ToString("N");
                _Path = _Annex.Annex_Path.Replace("/", "//");
                var _PathExists = _Path + "//" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;
                if (await _UnitOfWorkAnnexService._IFtpService.Exists(_PathExists))
                {
                    await _UnitOfWorkAnnexService._IFtpService.DeleteImage(_PathExists);
                }
                var _boolUpload = await _UnitOfWorkAnnexService._IFtpService.UploadImage(IFileUser.OpenReadStream(), _PhysicyNameFile, Path.GetExtension(IFileUser.FileName), _Path);
                if (!_boolUpload) return Ok(new { Message = MessageException.Messages.NoUploadUser.ToString(), Status = MessageException.Status.Status400 });
                _Annex.Annex_FileNameLogic = IFileUser.FileName;
                _Annex.Annex_FileExtension = Path.GetExtension(IFileUser.FileName);
                _Annex.Annex_FileNamePhysicy = _PhysicyNameFile;
                _UnitOfWorkAnnexService._IAnnexsService.Update(_Annex);
                var _PathNew = (_Path + "//" + _PhysicyNameFile + "" + Path.GetExtension(IFileUser.FileName)).Replace("//", "/");
                if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Path = _PathNew, Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }
            else
            {
                string _PhysicyNameFile = Guid.NewGuid().ToString("N");
                _Path = _DtoAnnexSetting.AnnexSetting_Path + "//" + _User.User_ID;
                var _boolDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_Path);
                var _boolUpload = await _UnitOfWorkAnnexService._IFtpService.UploadImage(IFileUser.OpenReadStream(), _PhysicyNameFile, Path.GetExtension(IFileUser.FileName), _Path);
                if (!_boolUpload) return Ok(new { Message = MessageException.Messages.NoUploadUser.ToString(), Status = MessageException.Status.Status400 });
                Annexs Annex_ = new Annexs()
                {
                    Annex_Path = _Path.Replace("//", "/"),
                    Annex_FileNamePhysicy = _PhysicyNameFile,
                    Annex_FileNameLogic = IFileUser.FileName,
                    Annex_ReferenceID = _User.User_ID,
                    Annex_CreatedDate = DateTime.Now,
                    Annex_Description = "",
                    Annex_ReferenceFolderName = _User.User_FirstName + " " + _User.User_LastName,
                    Annex_AnnexSettingID = _DtoAnnexSetting.AnnexSetting_ID,
                    Annex_FileExtension = Path.GetExtension(IFileUser.FileName),
                    Annex_IsDefault = true
                };
                await _UnitOfWorkAnnexService._IAnnexsService.Insert(Annex_);
                _Path = (_Path + "//" + Annex_.Annex_FileNamePhysicy + "" + Annex_.Annex_FileExtension).Replace("//", "/");
                if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Path = _Path, Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }


        [AuthorizeCommon]
        [Route("DeleteImage")]
        [HttpGet]
        public async Task<IActionResult> DeleteImage()
        {
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == _UserID);
            if (_User == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_User.User_TenantID, "USRA");
            var _AnnexSeting_ID = _DtoAnnexSetting.AnnexSetting_ID;
            var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_AnnexSettingID == _AnnexSeting_ID && A.Annex_ReferenceID == _User.User_ID);
            var _Path = "";
            if (_Annex != null) _Path = _Annex.Annex_Path + "/" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;
            if (await _UnitOfWorkAnnexService._IFtpService.Exists(_Path))
            {
                await _UnitOfWorkAnnexService._IFtpService.DeleteImage(_Path);
                _UnitOfWorkAnnexService._IAnnexsService.Delete(_Annex);
                if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }


        [Route("GetAllProducts")]
        [HttpPost]
        public async Task<IActionResult> GetAllProducts([FromForm] String? SearchText, [FromForm] DtoFilterProduct? Filters)
        {
            DataTable DtFilterProducts = new DataTable();
            DtFilterProducts.Columns.Add("TF_FieldName");
            DtFilterProducts.Columns.Add("TF_Condition");

            if (SearchText == null) SearchText = "";
            DtFilterProducts.Rows.Add();
            DtFilterProducts.Rows[0]["TF_FieldName"] = "SearchText";
            DtFilterProducts.Rows[0]["TF_Condition"] = SearchText;

            var ProductGroup = "";
            if (Filters.Filter_ProductGroup != null)
                ProductGroup = string.Join(',', Filters.Filter_ProductGroup.ToArray());
            DtFilterProducts.Rows.Add();
            DtFilterProducts.Rows[1]["TF_FieldName"] = "Product_Group";
            DtFilterProducts.Rows[1]["TF_Condition"] = ProductGroup;

            DtFilterProducts.Rows.Add();
            DtFilterProducts.Rows[2]["TF_FieldName"] = "Product_Type";
            DtFilterProducts.Rows[2]["TF_Condition"] = Filters.Filter_ProductsType;

            var ProductsIsUsed = "";
            if (Filters.Filter_ProductsIsUsed != null)
                ProductsIsUsed = string.Join(',', Filters.Filter_ProductsIsUsed.ToArray());
            DtFilterProducts.Rows.Add();
            DtFilterProducts.Rows[3]["TF_FieldName"] = "Product_IsUsed";
            DtFilterProducts.Rows[3]["TF_Condition"] = ProductsIsUsed;

            var _Products = await _UnitOfWorkWCSService._IProductService.GetAll_SP(DtFilterProducts);
            if (_Products == null) return Ok(new { Message = MessageException.Messages.RequestNull, Status = MessageException.Status.Status400 });
            return Ok(new { Products = _Products, Status = MessageException.Status.Status200 });
        }


        [Route("GetAllProductGroup")]
        [HttpPost]
        public async Task<IActionResult> GetAllProductGroup([FromForm] int? TypeP)
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


        [Route("GetAllFiles")]
        [HttpPost]
        public async Task<IActionResult> GetAllFiles([FromForm] int ProductID)
        {
            if (ProductID <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _AnnexSettingCARPA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CARPA");
            var DtoListFiles = _UnitOfWorkAnnexService._IAnnexsService.GetAllFiles(ProductID, _AnnexSettingCARPA.AnnexSetting_ID);
            if (DtoListFiles == null) return Ok(new { Message = MessageException.Messages.NullFilesProduct.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { FilesProduct = DtoListFiles, Status = MessageException.Status.Status200 });
        }


        [Route("DetailByID")]
        [HttpPost]
        public async Task<IActionResult> DetailByID([FromForm] int PID)
        {
            if (PID <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _Product = await _UnitOfWorkWCSService._IProductService.GetByWhere(P => P.Product_ID == PID);
            if (_Product == null) return Ok(new { Message = MessageException.Messages.NullProduct.ToString(), Status = MessageException.Status.Status400 });
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _AnnexSettingCARPA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CARPA");
            var _DtoListFiles = await _UnitOfWorkAnnexService._IAnnexsService.GetAllFiles(PID, _AnnexSettingCARPA.AnnexSetting_ID);
            return Ok(new { Product = _Product, ListFiles = _DtoListFiles, Status = MessageException.Status.Status200 });
        }

        [Route("GetServerTime")]
        [HttpGet]
        public IActionResult GetServerTime()
        {
            return Ok(new { DateTime = DateTime.Now , Status = MessageException.Status.Status200 });
        }


        /* Not necessary
        [Route("DeleteFile")]
        [HttpPost]
        public async Task<IActionResult> DeleteFile([FromForm] int AnnexID)
        {
            if (AnnexID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            // Delete File 
            var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_ID == AnnexID);
            if (_Annex == null) return Ok(new { Message = MessageException.Messages.NullAnnex.ToString(), Status = MessageException.Status.Status400 });
            var _Path = _Annex.Annex_Path + "/" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;
            await _UnitOfWorkAnnexService._IFtpService.DeleteImage(_Path);
            _UnitOfWorkAnnexService._IAnnexsService.Delete(_Annex);
            if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status200 });
        }
        */
    }
}
