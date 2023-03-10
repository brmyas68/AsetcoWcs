using AutoMapper;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;

namespace WCS.Common.Mapping
{
    public class MapperWornMaster
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(W =>
            {
                W.CreateMap<WornMaster, DtoWornMaster>()
                      .ForMember(DtoW => DtoW.WMstr_ID, opt => opt.MapFrom(W => W.WornMaster_ID))
                      .ForMember(DtoW => DtoW.WMstr_OwnID, opt => opt.MapFrom(W => W.WornMaster_OwnerID))
                      .ForMember(DtoW => DtoW.WMstr_AgtID, opt => opt.MapFrom(W => W.WornMaster_AgentID))
                      .ForMember(DtoW => DtoW.WMstr_BusiID, opt => opt.MapFrom(W => W.WornMaster_BusinessID))
                      .ForMember(DtoW => DtoW.WMstr_WCarID, opt => opt.MapFrom(W => W.WornMaster_WornCarID))
                      .ForMember(DtoW => DtoW.WMstr_WCarState, opt => opt.MapFrom(W => W.WornMaster_WornCarState))
                      .ForMember(DtoW => DtoW.WMstr_CvilConID, opt => opt.MapFrom(W => W.WornMaster_CivilContractID))
                      .ForMember(DtoW => DtoW.WMstr_RegDate, opt => opt.MapFrom(W => W.WornMaster_RegisterDate))
                      .ForMember(DtoW => DtoW.WMstr_TrckCode, opt => opt.MapFrom(W => W.WornMaster_TrackingCode))
                      .ForMember(DtoW => DtoW.WMstr_State, opt => opt.MapFrom(W => W.WornMaster_State))
                      .ForMember(DtoW => DtoW.WMstr_StateDesc, opt => opt.MapFrom(W => W.WornMaster_StateDesc))
                      .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}
