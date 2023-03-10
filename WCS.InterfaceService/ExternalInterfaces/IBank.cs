

using WCS.ClassDTO.DTOs.Bank;

namespace WCS.InterfaceService.ExternalInterfaces
{
    public interface IBank
    {
        Task<ResultPeyment> Payment(BodyPayment BodyPayment);
        Task<ResultConfirm> Confirm(BodyConfirm BodyConfirm);
        Task<ResultReverse> Reverse(BodyReverse BodyReverse); 
    }
}
