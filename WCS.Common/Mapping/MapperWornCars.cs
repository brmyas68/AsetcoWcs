using AutoMapper;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;

namespace WCS.Common.Mapping
{
    public class MapperWornCars
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(W =>
            {
                W.CreateMap<WornCars, DtoWornCars>()
                      .ForMember(DtoW => DtoW.WCars_ID, opt => opt.MapFrom(W => W.WornCars_ID))
                     // .ForMember(DtoW => DtoW.WCars_BrdID, opt => opt.MapFrom(W => W.WornCars_BrandID))
                      .ForMember(DtoW => DtoW.WCars_ModID, opt => opt.MapFrom(W => W.WornCars_ModelID))
                      .ForMember(DtoW => DtoW.WCars_BldYear, opt => opt.MapFrom(W => W.WornCars_BuildYear))
                      .ForMember(DtoW => DtoW.WCars_Desc, opt => opt.MapFrom(W => W.WornCars_Desc))
                      .ForMember(DtoW => DtoW.WCars_UsrType, opt => opt.MapFrom(W => W.WornCars_UserType))
                      .ForMember(DtoW => DtoW.WCars_State, opt => opt.MapFrom(W => W.WornCars_State))
                      .ForMember(DtoW => DtoW.WCars_DocType, opt => opt.MapFrom(W => W.WornCars_DocumentType))
                      .ForMember(DtoW => DtoW.WCars_Weight, opt => opt.MapFrom(W => W.WornCars_Weight))
                      .ForMember(DtoW => DtoW.WCars_PlkType, opt => opt.MapFrom(W => W.WornCars_PelakType))
                      .ForMember(DtoW => DtoW.WCars_SellType, opt => opt.MapFrom(W => W.WornCars_SellType))

                      .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}
