using AutoMapper;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;

namespace WCS.Common.Mapping
{
    public class MapperOwners
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(O =>
            {
                O.CreateMap<Owners, DtoOwners>()
                      .ForMember(DtoO => DtoO.Own_ID, opt => opt.MapFrom(O => O.Owners_ID))
                      .ForMember(DtoO => DtoO.Own_UsrID, opt => opt.MapFrom(O => O.Owners_UserID))
                      .ForMember(DtoO => DtoO.Own_Tell, opt => opt.MapFrom(O => O.Owners_Tell))
                      .ForMember(DtoO => DtoO.Own_Desc, opt => opt.MapFrom(O => O.Owners_Desc))
                      .ForMember(DtoO => DtoO.Own_ShbaNum, opt => opt.MapFrom(O => O.Owners_ShabaNumber))
                      .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}
