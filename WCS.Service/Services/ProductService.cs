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
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly ContextWCS _ContextWCS;
        public ProductService(ContextWCS ContextWCS) : base(ContextWCS)
        {
            _ContextWCS = ContextWCS;
        }

        public async Task<List<DtoBuyProducts_>> GetAll_SP(DataTable Dt)
        {
            
            var _Cmd = _ContextWCS.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPWCS.SPName.Wcs_GetAllProduct.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter ParameterDetails = new SqlParameter();
            ParameterDetails.ParameterName = "Details";
            ParameterDetails.SqlDbType = SqlDbType.Structured;
            ParameterDetails.Value = Dt;

            _Cmd.Parameters.Add(ParameterDetails);

            var _ListProducts = new List<DtoBuyProducts_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _ListProducts.Add(
                            new DtoBuyProducts_
                            {
                                P_ID = Convert.ToInt32(_Reader["Product_ID"]),
                                P_Model = _Reader["Product_Model"].ToString(),
                                P_Name = _Reader["Product_Name"].ToString(),
                                P_NameEn = _Reader["Product_NameEn"].ToString(),
                                P_Path = _Reader["Path"].ToString(),
                                P_Price = Convert.ToInt64(_Reader["Product_Price"]),
                                P_Type = Convert.ToInt64(_Reader["Product_Type"]),
                                P_Date = _Reader["Product_RegisterDate"].ToString(),
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
            return _ListProducts;

        }

        public async Task<List<Product>> GetAll(Int16 Type)
        {
            return await _ContextWCS.Products.Where(p => p.Product_Type == Type).ToListAsync();
        }

        public async Task<List<DtoBuyProducts_>> GetDetailProducts(List<int> ProductSID)
        {
            var _Cmd = _ContextWCS.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPWCS.SPName.Wcs_DetailListBuyProducts.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter Parameter = new SqlParameter();
            Parameter.ParameterName = "@ProductsID";
            Parameter.SqlDbType = SqlDbType.NVarChar;
            Parameter.Value = string.Join(',', ProductSID.ToArray());
            _Cmd.Parameters.Add(Parameter);

            var _BuyProducts = new List<DtoBuyProducts_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _BuyProducts.Add(
                            new DtoBuyProducts_
                            {
                                P_ID = Convert.ToInt32(_Reader["Product_ID"]),
                                P_Model = _Reader["Product_Model"].ToString(),
                                P_Name = _Reader["Product_Name"].ToString(),
                                P_NameEn = _Reader["Product_NameEn"].ToString(),
                                P_Path = _Reader["Path"].ToString(),
                                P_Price = Convert.ToInt64(_Reader["Product_Price"]),
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
            return _BuyProducts;
        }
    }
}
