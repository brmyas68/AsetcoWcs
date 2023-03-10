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
    public class ProductController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private IUnitOfWorkWCSService _UnitOfWorkWCSService; private IUnitOfWorkAnnexService _UnitOfWorkAnnexService; private readonly IConfiguration _Configuration; IMapper _IMapper;
        public ProductController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkWCSService UnitOfWorkWCSService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkWCSService = UnitOfWorkWCSService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _IMapper = MapperProduct.MapTo(); }

        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Product, EnumPermission.Actions.Action_WCS_Product_GetAll)]//[AllowAnonymous]
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm] String? SearchText, [FromForm] DtoFilterProduct? Filters)
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


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Product, EnumPermission.Actions.Action_WCS_Product_Insert)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoProduct _DtoProducts, [FromForm] IFormFileCollection ProductFiles)
        {
            if (_DtoProducts == null) return Ok(new { Message = MessageException.Messages.NullProduct.ToString(), Status = MessageException.Status.Status400 });
            _DtoProducts.Prod_RegDate = DateTime.Now;
            var _Product = _IMapper.Map<DtoProduct, Product>(_DtoProducts);
            await _UnitOfWorkWCSService._IProductService.Insert(_Product);
            if (!(await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0)) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });

            var _ProductPrice = new ProductPrice()
            {
                ProductPrice_ProductId = _Product.Product_ID,
                ProductPrice_Price = _Product.Product_Price,
                ProductPrice_Date = DateTime.Now,
            };
            await _UnitOfWorkWCSService._IProductPriceService.Insert(_ProductPrice);
            await _UnitOfWorkWCSService.SaveChange_DataBase_Async();

            // upload Files  
            if (ProductFiles.Count() > 0)
            {
                var DtoListFiles = new List<DtoListFiles>();
                var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
                var _AnnexSettingCARPA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CARPA");
                foreach (var item in ProductFiles)
                {
                    var DtoFiles = new DtoListFiles()
                    {
                        FileNameLogic = item.FileName,
                        ExtensionName = Path.GetExtension(item.FileName),
                        FileNamePhysic = Guid.NewGuid().ToString("N"),
                        FileStream = item.OpenReadStream(),
                        FileDesc = _DtoProducts.Prod_Name,

                    };
                    DtoListFiles.Add(DtoFiles);
                }
                var _path = (_AnnexSettingCARPA.AnnexSetting_Path + "//" + _Product.Product_ID).ToString();
                var _stateDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_path.Replace("//", "/"));
                var AnnexList = await _UnitOfWorkAnnexService._IFtpService.UploadListFiles(DtoListFiles, _path, _Product.Product_ID, _AnnexSettingCARPA.AnnexSetting_ID);
                await _UnitOfWorkAnnexService._IAnnexsService.InsertRange(AnnexList);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
            }

            return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Product, EnumPermission.Actions.Action_WCS_Product_Update)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoProduct _DtoProducts)
        {
            if (_DtoProducts == null) return Ok(new { Message = MessageException.Messages.NullProduct.ToString(), Status = MessageException.Status.Status400 });
            var _Product = await _UnitOfWorkWCSService._IProductService.GetByWhere(P => P.Product_ID == _DtoProducts.Pro_ID);
            if (_Product == null) return Ok(new { Message = MessageException.Messages.NullProduct.ToString(), Status = MessageException.Status.Status400 });
            _Product.Product_Group = _DtoProducts.Prod_Group;
            _Product.Product_Name = _DtoProducts.Prod_Name;
            _Product.Product_NameEn = _DtoProducts.Prod_NameEn;
         //   _Product.Product_Price = _DtoProducts.Prod_Price;
            _Product.Product_Description = _DtoProducts.Prod_Desc;
            _Product.Product_Model = _DtoProducts.Prod_Model;
            _Product.Product_IsUsed = _DtoProducts.Prod_IsUsed;
            _Product.Product_Series = _DtoProducts.Prod_Serie;
            _Product.Product_Type = _DtoProducts.Prod_Type;
            _UnitOfWorkWCSService._IProductService.Update(_Product);
            if (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Product, EnumPermission.Actions.Action_WCS_Product_Delete)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int ProdId)
        {
            if (ProdId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _AnnexSettingCARPA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CARPA");
            var _Product = await _UnitOfWorkWCSService._IProductService.GetByWhere(P => P.Product_ID == ProdId);
            var State = false;
            if (_Product != null)
            {
                _UnitOfWorkWCSService._IProductService.Delete(_Product);
                State = (await _UnitOfWorkWCSService.SaveChange_DataBase_Async() > 0);
            }
            var _ProductsPrice = await _UnitOfWorkWCSService._IProductPriceService.GetAll(P => P.ProductPrice_ProductId == ProdId);
            if (_ProductsPrice.Any())
            {
                _UnitOfWorkWCSService._IProductPriceService.DeleteRange(_ProductsPrice);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }

            await _UnitOfWorkAnnexService._IFtpService.DeleteDirectoryFiles(_AnnexSettingCARPA.AnnexSetting_Path + "//" + ProdId + "//");
            var ListAnnex = await _UnitOfWorkAnnexService._IAnnexsService.GetAll(A => A.Annex_ReferenceID == ProdId & A.Annex_AnnexSettingID == _AnnexSettingCARPA.AnnexSetting_ID);
            if (ListAnnex != null)
            {
                _UnitOfWorkAnnexService._IAnnexsService.DeleteRange(ListAnnex);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
            }
            if (State) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
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
            var _ProductPrice = await _UnitOfWorkWCSService._IProductPriceService.GetLastProductPrice(PID);
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _AnnexSettingCARPA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CARPA");
            var _DtoListFiles = await _UnitOfWorkAnnexService._IAnnexsService.GetAllFiles(PID, _AnnexSettingCARPA.AnnexSetting_ID);
            return Ok(new { Product = _Product, ProductPrice = _ProductPrice, ListFiles = _DtoListFiles, Status = MessageException.Status.Status200 });
        }


       // [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Product, EnumPermission.Actions.Action_WCS_Product_BuyDetailProducts)]
        [Route("BuyDetailProducts")]
        [HttpPost]
        public async Task<IActionResult> BuyDetailProducts([FromForm] List<int> ProductsID)
        {
            if (!(ProductsID.Any())) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _ProductDetails = await _UnitOfWorkWCSService._IProductService.GetDetailProducts(ProductsID);
            if (_ProductDetails == null) return Ok(new { Message = MessageException.Messages.NullProduct.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { ProductDetails = _ProductDetails, Status = MessageException.Status.Status200 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Product, EnumPermission.Actions.Action_WCS_Product_UploadFile)]
        [Route("UploadFile")]
        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] int ProductID, [FromForm] IFormFileCollection ProductFiles)
        {
            if (ProductID <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            // upload Files  
            if (ProductFiles.Any())
            {
                var DtoListFiles = new List<DtoListFiles>();
                var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
                var _AnnexSettingCARPA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "CARPA");
                foreach (var item in ProductFiles)
                {
                    var DtoFiles = new DtoListFiles()
                    {
                        FileNameLogic = item.FileName,
                        ExtensionName = Path.GetExtension(item.FileName),
                        FileNamePhysic = Guid.NewGuid().ToString("N"),
                        FileStream = item.OpenReadStream(),
                        FileDesc = ProductID.ToString(),

                    };
                    DtoListFiles.Add(DtoFiles);
                }
                var _path = (_AnnexSettingCARPA.AnnexSetting_Path + "//" + ProductID).ToString();
                var _stateDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_path.Replace("//", "/"));
                var AnnexList = await _UnitOfWorkAnnexService._IFtpService.UploadListFiles(DtoListFiles, _path, ProductID, _AnnexSettingCARPA.AnnexSetting_ID);
                await _UnitOfWorkAnnexService._IAnnexsService.InsertRange(AnnexList);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
            }

            return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_WCS_Product, EnumPermission.Actions.Action_WCS_Product_DeleteFile)]
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
