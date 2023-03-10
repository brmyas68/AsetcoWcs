

using System.Data;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs.Customs;
using WCS.InterfaceService.InterfacesBase;

namespace WCS.InterfaceService.Interfaces
{
    public interface IWornMasterService : IBaseService<WornMaster>
    {
        string GenerateTrackingCode();
        Task<List<WornCars_>> GetAllWornCars(int UserID);
        Task<List<DtoWornMaster_>> GetAll_SP(DataTable Dt, int UserID);

        Task<DtoWornMaster_> GetByWMaster_SP(int WMasterID);

    }
}
