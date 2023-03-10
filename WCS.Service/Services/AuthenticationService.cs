



using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using UC.DataLayer.Contex;
using WCS.Common.StoredProcedure;
using WCS.InterfaceService.Interfaces;

namespace WCS.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ContextUC _ContextUC;
        public AuthenticationService()
        {
            _ContextUC = new ContextUC();
        }

        public async Task<int> CheckPermissions(int User_ID, int Tenant_ID, string TagName_Form, string TagName_Action)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPWCS.SPName.UC_CheckPermissions.ToString();
            _Cmd.Parameters.Add(new SqlParameter("UserID", User_ID));
            _Cmd.Parameters.Add(new SqlParameter("TenantID", Tenant_ID));
            _Cmd.Parameters.Add(new SqlParameter("TagNameForm", TagName_Form));
            _Cmd.Parameters.Add(new SqlParameter("TagNameAction", TagName_Action));
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var _CountFound = 0;
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _CountFound = Convert.ToInt32(_Reader["Count_Found"]);
                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _CountFound;
        }
    }
}
