

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs.Customs;
using WCS.Common.StoredProcedure;
using WCS.DataLayer.Contex;
using WCS.InterfaceService.Interfaces;
using WCS.Service.ServiceBase;

namespace WCS.Service.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly ContextWCS _ContextWCS;
        public OrderService(ContextWCS ContextWCS) : base(ContextWCS)
        {
            _ContextWCS = ContextWCS;
        }

        public async Task<List<DtoOrders>> GetAll_SP(System.Data.DataTable Dt, int UserID)
        {
            var _Cmd = _ContextWCS.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPWCS.SPName.Wcs_GetAllOrders.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter ParameterDetails = new SqlParameter();
            ParameterDetails.ParameterName = "Details";
            ParameterDetails.SqlDbType = SqlDbType.Structured;
            ParameterDetails.Value = Dt;
            _Cmd.Parameters.Add(ParameterDetails);

            SqlParameter ParameterUserID = new SqlParameter();
            ParameterUserID.ParameterName = "UserID";
            ParameterUserID.SqlDbType = SqlDbType.Int;
            ParameterUserID.Value = UserID;
            _Cmd.Parameters.Add(ParameterUserID);

            var _DtoOrders = new List<DtoOrders>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoOrders.Add(
                            new DtoOrders
                            {
                                Ord_ID = _Reader["Ord_ID"].ToString(),
                                Ord_PID = Convert.ToInt32(_Reader["Product_ID"]),
                                Ord_Name = _Reader["Ord_Name"].ToString(),
                                Ord_FullName = _Reader["Ord_FullName"].ToString(),
                                Ord_CustomerFname = _Reader["Ord_CustomerFname"].ToString(),
                                Ord_CustomerLname = _Reader["Ord_CustomerLname"].ToString(),
                                Ord_CustomerMobile = _Reader["Ord_CustomerMobile"].ToString(),
                                Ord_Date = _Reader["Ord_Date"].ToString(),
                                Ord_Description = _Reader["Ord_Description"].ToString(),
                                Ord_Group = _Reader["Ord_Group"].ToString(),
                                Ord_IsUsed = _Reader["Ord_IsUsed"].ToString(),
                                Ord_IsUsedStr = _Reader["Ord_IsUsedStr"].ToString(),
                                Ord_Model = _Reader["Ord_Model"].ToString(),
                                Ord_ResultComment = Convert.ToInt16(_Reader["Ord_ResultComment"]),
                                Ord_PType = Convert.ToInt16(_Reader["Ord_PType"]),
                                Ord_Series = _Reader["Ord_Series"].ToString(),
                                Ord_Type = _Reader["Ord_Type"].ToString(),
                                Ord_TypeStr = _Reader["Ord_TypeStr"].ToString(),
                                Ord_Price = _Reader["Ord_Price"].ToString(),
                                Ord_Count = _Reader["Ord_Count"].ToString(),
                                Ord_TotalPrice = Convert.ToInt64(_Reader["Ord_Price"]) * Convert.ToInt64(_Reader["Ord_Count"]),
                                Total_Count = _Reader["Total_Count"].ToString(),
                                Ord_Code = _Reader["Ord_Code"].ToString(),
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
            return _DtoOrders;
        }

        public async Task<DtoOrders> GetByOrder_SP(int OrderID)
        {
            var _Cmd = _ContextWCS.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPWCS.SPName.Wcs_GetByOrder.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter Parameter = new SqlParameter();
            Parameter.ParameterName = "OrderID";
            Parameter.SqlDbType = SqlDbType.Int;
            Parameter.Value = OrderID;
            _Cmd.Parameters.Add(Parameter);

            var _DtoOrder = new DtoOrders();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {

                        _DtoOrder.Ord_ID = _Reader["Ord_ID"].ToString();
                        _DtoOrder.Ord_PID = Convert.ToInt32(_Reader["Product_ID"]);
                        _DtoOrder.Ord_Name = _Reader["Ord_Name"].ToString();
                        _DtoOrder.Ord_FullName = _Reader["Ord_FullName"].ToString();
                        _DtoOrder.Ord_CustomerFname = _Reader["Ord_CustomerFname"].ToString();
                        _DtoOrder.Ord_CustomerLname = _Reader["Ord_CustomerLname"].ToString();
                        _DtoOrder.Ord_CustomerMobile = _Reader["Ord_CustomerMobile"].ToString();
                        _DtoOrder.Ord_Date = _Reader["Ord_Date"].ToString();
                        _DtoOrder.Ord_Description = _Reader["Ord_Description"].ToString();
                        _DtoOrder.Ord_Group = _Reader["Ord_Group"].ToString();
                        _DtoOrder.Ord_IsUsed = _Reader["Ord_IsUsed"].ToString();
                        _DtoOrder.Ord_IsUsedStr = _Reader["Ord_IsUsedStr"].ToString();
                        _DtoOrder.Ord_Model = _Reader["Ord_Model"].ToString();
                        _DtoOrder.Ord_ResultComment = Convert.ToInt16(_Reader["Ord_ResultComment"]);
                        _DtoOrder.Ord_PType = Convert.ToInt16(_Reader["Ord_PType"]);
                        _DtoOrder.Ord_Series = _Reader["Ord_Series"].ToString();
                        _DtoOrder.Ord_Type = _Reader["Ord_Type"].ToString();
                        _DtoOrder.Ord_TypeStr = _Reader["Ord_TypeStr"].ToString();
                        _DtoOrder.Ord_Price = _Reader["Ord_Price"].ToString();
                        _DtoOrder.Ord_Count = _Reader["Ord_Count"].ToString();
                        _DtoOrder.Ord_Code = _Reader["Ord_Code"].ToString();
                        _DtoOrder.Ord_TotalPrice = Convert.ToInt64(_Reader["Ord_Price"]) * Convert.ToInt64(_Reader["Ord_Count"]);

                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _DtoOrder;
        }
    }
}
