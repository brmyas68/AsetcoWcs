

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
    public class WornMasterService : BaseService<WornMaster>, IWornMasterService
    {
        private readonly ContextWCS _ContextWCS;
        public WornMasterService(ContextWCS ContextWCS) : base(ContextWCS)
        {
            _ContextWCS = ContextWCS;
        }

        public string GenerateTrackingCode()
        {
            Random _random = new Random();

            int Num1 = _random.Next(100, 999);
            int Num2 = _random.Next(10, 99);
            int Num3 = _random.Next(1, 9);

            char offsetL = 'a';
            char offsetU = 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            var char1 = (char)_random.Next(offsetL, offsetL + lettersOffset);
            var char2 = (char)_random.Next(offsetU, offsetU + lettersOffset);

            return Num1.ToString() + "" + char1.ToString() + "@" + Num2.ToString() + "" + char2.ToString() + "" + char1.ToString() + "" + Num3.ToString();
        }

        public async Task<List<WornCars_>> GetAllWornCars(int UserID)
        {
            var _Cmd = _ContextWCS.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPWCS.SPName.Wcs_GetAllWornCars.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter Parameter = new SqlParameter();
            Parameter.ParameterName = "UserID";
            Parameter.SqlDbType = SqlDbType.Int;
            Parameter.Value = UserID;
            _Cmd.Parameters.Add(Parameter);

            var _WornCars = new List<WornCars_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _WornCars.Add(
                            new WornCars_
                            {
                                WCars_Id = Convert.ToInt32(_Reader["WCars_Id"]),
                                WCars_Name = _Reader["WCars_Name"].ToString(),

                            }); ;
                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _WornCars;
        }


        public async Task<List<DtoWornMaster_>> GetAll_SP(System.Data.DataTable Dt, int UserID)
        {
            var _Cmd = _ContextWCS.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPWCS.SPName.Wcs_GetAll.ToString();
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

            var _DtoWornMaster = new List<DtoWornMaster_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoWornMaster.Add(
                            new DtoWornMaster_
                            {
                                Wm_ID = Convert.ToInt32(_Reader["WornMaster_ID"]),
                                Wm_FName = _Reader["Wm_FName"].ToString(),
                                Wm_LName = _Reader["Wm_LName"].ToString(),
                                Wm_Mobile = _Reader["Wm_Mobile"].ToString(),
                                B_AgentName = _Reader["B_AgentName"].ToString(),
                                B_MaxPrice = _Reader["B_MaxPrice"].ToString(),
                                B_MinPrice = _Reader["B_MinPrice"].ToString(),
                                C_BuyAmount = _Reader["C_BuyAmount"].ToString(),
                                C_ContractState = _Reader["C_ContractState"].ToString(),
                                C_ContractStateStr = _Reader["C_ContractStateStr"].ToString(),
                                C_InquiryState = _Reader["C_InquiryState"].ToString(),
                                C_InquiryStateStr = _Reader["C_InquiryStateStr"].ToString(),
                                C_Parking = _Reader["C_Parking"].ToString(),
                                C_PreAmount = _Reader["C_PreAmount"].ToString(),
                                C_SplitCenter = _Reader["C_SplitCenter"].ToString(),
                                C_WornCarState = _Reader["C_WornCarState"].ToString(),
                                C_WornCarStateStr = _Reader["C_WornCarStateStr"].ToString(),
                                B_AgreePrice = Convert.ToInt64(_Reader["B_AgreePrice"]),
                                B_AgreePrePrice = Convert.ToInt64(_Reader["B_AgreePrePrice"]),
                                B_Investor = Convert.ToInt32(_Reader["B_Investor"]),
                                F_OtherPayed = _Reader["F_OtherPayed"].ToString(),
                                F_Payed = _Reader["F_Payed"].ToString(),
                                Total_Count = _Reader["Total_Count"].ToString(),
                                Wm_Address = _Reader["Wm_Address"].ToString(),
                                Wm_CtyName = _Reader["Wm_CtyName"].ToString(),
                                Wm_DateReg = _Reader["Wm_DateReg"].ToString(),
                                Wm_IdentNum = _Reader["Wm_IdentNum"].ToString(),
                                Wm_ProvName = _Reader["Wm_ProvName"].ToString(),
                                Wm_UName = _Reader["Wm_UName"].ToString(),
                                Wm_User = _Reader["Wm_User"].ToString(),
                                Wm_UserCtyID = _Reader["Wm_UserCtyID"].ToString(),
                                Wm_UserProvID = _Reader["Wm_UserProvID"].ToString(),
                                WornCarID = Convert.ToInt32(_Reader["WornCars_ID"].ToString()),
                                WornCarName = _Reader["WornCarName"].ToString(),
                                F_Remaining = _Reader["F_Remaining"].ToString(),
                                WM_Desc = _Reader["WornMaster_StateDesc"].ToString(),
                                InqValidationInq = Convert.ToInt16(_Reader["InqValidationInq"]),
                                InqValidationContract = Convert.ToInt16(_Reader["InqValidationContract"]),
                                InqValidationDoc = Convert.ToInt16(_Reader["InqValidationDoc"]),
                                C_WornCarsSellType = Convert.ToBoolean(_Reader["C_WornCarsSellType"]),
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
            return _DtoWornMaster;
        }

        public async Task<DtoWornMaster_> GetByWMaster_SP(int WMasterID)
        {
            var _Cmd = _ContextWCS.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPWCS.SPName.Wcs_GetByWornMaster.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;


            SqlParameter Parameter = new SqlParameter();
            Parameter.ParameterName = "WMasterID";
            Parameter.SqlDbType = SqlDbType.Int;
            Parameter.Value = WMasterID;
            _Cmd.Parameters.Add(Parameter);

            var _DtoWornMaster = new DtoWornMaster_();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoWornMaster.Wm_ID = Convert.ToInt32(_Reader["WornMaster_ID"]);
                        _DtoWornMaster.Wm_FName = _Reader["Wm_FName"].ToString();
                        _DtoWornMaster.Wm_LName = _Reader["Wm_LName"].ToString();
                        _DtoWornMaster.Wm_Mobile = _Reader["Wm_Mobile"].ToString();
                        _DtoWornMaster.B_AgentName = _Reader["B_AgentName"].ToString();
                        _DtoWornMaster.B_MaxPrice = _Reader["B_MaxPrice"].ToString();
                        _DtoWornMaster.B_MinPrice = _Reader["B_MinPrice"].ToString();
                        _DtoWornMaster.C_BuyAmount = _Reader["C_BuyAmount"].ToString();
                        _DtoWornMaster.C_ContractState = _Reader["C_ContractState"].ToString();
                        _DtoWornMaster.C_ContractStateStr = _Reader["C_ContractStateStr"].ToString();
                        _DtoWornMaster.C_InquiryState = _Reader["C_InquiryState"].ToString();
                        _DtoWornMaster.C_InquiryStateStr = _Reader["C_InquiryStateStr"].ToString();
                        _DtoWornMaster.C_Parking = _Reader["C_Parking"].ToString();
                        _DtoWornMaster.C_PreAmount = _Reader["C_PreAmount"].ToString();
                        _DtoWornMaster.C_SplitCenter = _Reader["C_SplitCenter"].ToString();
                        _DtoWornMaster.C_WornCarState = _Reader["C_WornCarState"].ToString();
                        _DtoWornMaster.C_WornCarStateStr = _Reader["C_WornCarStateStr"].ToString();
                        _DtoWornMaster.B_AgreePrice = Convert.ToInt64(_Reader["B_AgreePrice"]);
                        _DtoWornMaster.B_AgreePrePrice = Convert.ToInt64(_Reader["B_AgreePrePrice"]);
                        _DtoWornMaster.B_Investor = Convert.ToInt32(_Reader["B_Investor"]);
                        _DtoWornMaster.F_OtherPayed = _Reader["F_OtherPayed"].ToString();
                        _DtoWornMaster.F_Payed = _Reader["F_Payed"].ToString();
                        _DtoWornMaster.Wm_Address = _Reader["Wm_Address"].ToString();
                        _DtoWornMaster.Wm_CtyName = _Reader["Wm_CtyName"].ToString();
                        _DtoWornMaster.Wm_DateReg = _Reader["Wm_DateReg"].ToString();
                        _DtoWornMaster.Wm_IdentNum = _Reader["Wm_IdentNum"].ToString();
                        _DtoWornMaster.Wm_ProvName = _Reader["Wm_ProvName"].ToString();
                        _DtoWornMaster.Wm_UName = _Reader["Wm_UName"].ToString();
                        _DtoWornMaster.Wm_User = _Reader["Wm_User"].ToString();
                        _DtoWornMaster.Wm_UserCtyID = _Reader["Wm_UserCtyID"].ToString();
                        _DtoWornMaster.Wm_UserProvID = _Reader["Wm_UserProvID"].ToString();
                        _DtoWornMaster.WornCarID = Convert.ToInt32(_Reader["WornCars_ID"].ToString());
                        _DtoWornMaster.WornCarName = _Reader["WornCarName"].ToString();
                        _DtoWornMaster.F_Remaining = _Reader["F_Remaining"].ToString();
                        _DtoWornMaster.WM_Desc = _Reader["WornMaster_StateDesc"].ToString();
                        _DtoWornMaster.InqValidationInq = Convert.ToInt16(_Reader["InqValidationInq"]);
                        _DtoWornMaster.InqValidationContract = Convert.ToInt16(_Reader["InqValidationContract"]);
                        _DtoWornMaster.InqValidationDoc = Convert.ToInt16(_Reader["InqValidationDoc"]);
                        _DtoWornMaster.C_WornCarsSellType = Convert.ToBoolean(_Reader["C_WornCarsSellType"]);

                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _DtoWornMaster;
        }
    }
}
