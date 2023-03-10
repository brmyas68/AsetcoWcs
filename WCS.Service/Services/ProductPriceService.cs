using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs.Customs;
using WCS.ClassDTO.DTOs.Filters;
using WCS.Common.StoredProcedure;
using WCS.DataLayer.Contex;
using WCS.InterfaceService.Interfaces;
using WCS.Service.ServiceBase;

namespace WCS.Service.Services
{
    public class ProductPriceService : BaseService<ProductPrice>, IProductPriceService
    {
        private readonly ContextWCS _ContextWCS;
        public ProductPriceService(ContextWCS ContextWCS) : base(ContextWCS)
        {
            _ContextWCS = ContextWCS;
        }

        public async Task<List<DtoProductPrice_>> GetAll_SP(int? ProductId, string AddDate)
        {
            var _Cmd = _ContextWCS.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPWCS.SPName.Wcs_GetAllProductPrice.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter ParameterProductId = new SqlParameter();
            ParameterProductId.ParameterName = "@ProductId";
            ParameterProductId.SqlDbType = SqlDbType.Int;
            ParameterProductId.Value = ProductId != null ? ProductId.Value : 0;
            _Cmd.Parameters.Add(ParameterProductId);

            SqlParameter ParameterAddDate = new SqlParameter();
            ParameterAddDate.ParameterName = "@AddDate";
            ParameterAddDate.SqlDbType = SqlDbType.NVarChar;
            ParameterAddDate.Value = AddDate != null ? AddDate : "";
            _Cmd.Parameters.Add(ParameterAddDate);

            var _ListProductsPrice = new List<DtoProductPrice_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _ListProductsPrice.Add(
                            new DtoProductPrice_
                            {
                                ProPric_ID = Convert.ToInt32(_Reader["ProductPrice_ID"]),
                                ProPric_PId = Convert.ToInt32(_Reader["ProductPrice_ProductId"]),
                                ProPric_Price = Convert.ToInt64(_Reader["ProductPrice_Price"]),
                                ProPric_Date = _Reader["ProductPrice_Date"].ToString(),
                                ProPric_PName = _Reader["ProductPrice_PName"].ToString(),
                                ProPric_PGroup = _Reader["ProductPrice_PGroup"].ToString(),
                                ProPric_PGroupId = Convert.ToInt32(_Reader["ProductPrice_PGroupId"]),
                                ProPric_PType = Convert.ToInt16(_Reader["ProductPrice_PType"]),
                            });
                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _ListProductsPrice;

        }

        public async Task<DtoProductPrice_> GetPriceById_SP(int ProdPricId)
        {
            var _Cmd = _ContextWCS.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPWCS.SPName.Wcs_GetProductPriceById.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter Parameter = new SqlParameter();
            Parameter.ParameterName = "@ProdPricId";
            Parameter.SqlDbType = SqlDbType.Int;
            Parameter.Value = ProdPricId;
            _Cmd.Parameters.Add(Parameter);

            var _DtoProductPrice = new DtoProductPrice_();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {

                        _DtoProductPrice.ProPric_ID = Convert.ToInt32(_Reader["ProductPrice_ID"]);
                        _DtoProductPrice.ProPric_PId = Convert.ToInt32(_Reader["ProductPrice_ProductId"]);
                        _DtoProductPrice.ProPric_Price = Convert.ToInt64(_Reader["ProductPrice_Price"]);
                        _DtoProductPrice.ProPric_Date = _Reader["ProductPrice_Date"].ToString();
                        _DtoProductPrice.ProPric_PName = _Reader["ProductPrice_PName"].ToString();
                        _DtoProductPrice.ProPric_PGroup = _Reader["ProductPrice_PGroup"].ToString();
                        _DtoProductPrice.ProPric_PGroupId = Convert.ToInt32(_Reader["ProductPrice_PGroupId"]);
                        _DtoProductPrice.ProPric_PType = Convert.ToInt16(_Reader["ProductPrice_PType"]);
                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _DtoProductPrice;
        }

        public async Task<ProductPrice> GetLastProductPrice(int ProductId)
        {
            return await _ContextWCS.ProductPrice.OrderBy(P => P.ProductPrice_Date)
                .FirstOrDefaultAsync(P => P.ProductPrice_ProductId == ProductId).ConfigureAwait(false);
        }

    }
}
