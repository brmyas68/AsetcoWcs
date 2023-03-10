using AutoMapper;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;

namespace WCS.Common.Mapping
{
    public class MapperFinance
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(F =>
            {
                F.CreateMap<Finance, DtoFinance>()
                      .ForMember(DtoF => DtoF.Fin_ID, opt => opt.MapFrom(F => F.Finance_ID))
                      .ForMember(DtoF => DtoF.Fin_ModiID, opt => opt.MapFrom(F => F.Finance_ModifirID))
                      .ForMember(DtoF => DtoF.Fin_PayType, opt => opt.MapFrom(F => F.Finance_PaymentType))
                      .ForMember(DtoF => DtoF.Fin_WrnMastrID, opt => opt.MapFrom(F => F.Finance_WornMasterID))
                      .ForMember(DtoF => DtoF.Fin_Amont, opt => opt.MapFrom(F => F.Finance_Amount))
                      .ForMember(DtoF => DtoF.Fin_Desc, opt => opt.MapFrom(F => F.Finance_Desc))
                      .ForMember(DtoF => DtoF.Fin_ModiDate, opt => opt.MapFrom(F => F.Finance_ModifirDate))
                      .ForMember(DtoF => DtoF.Fin_RegDate, opt => opt.MapFrom(F => F.Finance_RegisterDate))
                      .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}
