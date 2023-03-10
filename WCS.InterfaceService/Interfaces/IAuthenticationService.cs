 

namespace WCS.InterfaceService.Interfaces
{
    public interface IAuthenticationService
    {
        Task<int> CheckPermissions(int User_ID, int Tenant_ID, string TagName_Form, string TagName_Action);
    }
}
