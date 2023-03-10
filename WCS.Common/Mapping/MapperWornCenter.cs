using AutoMapper;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;

namespace WCS.Common.Mapping
{
    public class MapperWornCenter
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(W =>
            {
                W.CreateMap<WornCenter, DtoWornCenter>()
                      .ForMember(DtoW => DtoW.WCntr_ID, opt => opt.MapFrom(W => W.WornCenter_ID))
                      .ForMember(DtoW => DtoW.WCntr_Name, opt => opt.MapFrom(W => W.WornCenter_Name))
                      .ForMember(DtoW => DtoW.WCntr_ProvID, opt => opt.MapFrom(W => W.WornCenter_ProvinceID))
                      .ForMember(DtoW => DtoW.WCntr_CtyID, opt => opt.MapFrom(W => W.WornCenter_CityID))
                      .ForMember(DtoW => DtoW.WCntr_Fax, opt => opt.MapFrom(W => W.WornCenter_Fax))
                      .ForMember(DtoW => DtoW.WCntr_Mail, opt => opt.MapFrom(W => W.WornCenter_Email))
                      .ForMember(DtoW => DtoW.WCntr_Tell, opt => opt.MapFrom(W => W.WornCenter_Tell))
                      .ForMember(DtoW => DtoW.WCntr_Address, opt => opt.MapFrom(W => W.WornCenter_Address))
                      .ForMember(DtoW => DtoW.WCntr_MangFullName, opt => opt.MapFrom(W => W.WornCenter_ManagerFullName))
                      .ForMember(DtoW => DtoW.WCntr_Type, opt => opt.MapFrom(W => W.WornCenter_Type))
                      .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}
