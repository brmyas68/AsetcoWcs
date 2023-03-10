using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;
using AutoMapper;

namespace WCS.Common.Mapping
{
    public class MapperCivilContract
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<CivilContract, DtoCivilContract>()
                     .ForMember(DtoC => DtoC.CvilCon_ID, opt => opt.MapFrom(C => C.CivilContract_ID))
                     .ForMember(DtoC => DtoC.CvilCon_InqDocState, opt => opt.MapFrom(C => C.CivilContract_InqDocumentState))
                     .ForMember(DtoC => DtoC.CvilCon_InqDocDesc, opt => opt.MapFrom(C => C.CivilContract_InqDocumentDesc))
                     .ForMember(DtoC => DtoC.CvilCon_InqViolAmont, opt => opt.MapFrom(C => C.CivilContract_InqViolationAmount))
                     .ForMember(DtoC => DtoC.CvilCon_InqViolState, opt => opt.MapFrom(C => C.CivilContract_InqViolationState))
                     .ForMember(DtoC => DtoC.CvilCon_InqBlkState, opt => opt.MapFrom(C => C.CivilContract_InqBlockState))
                     .ForMember(DtoC => DtoC.CvilCon_InqPolDesc, opt => opt.MapFrom(C => C.CivilContract_InqPoliceDesc))
                     .ForMember(DtoC => DtoC.CvilCon_InqValIdentifyNumber, opt => opt.MapFrom(C => C.CivilContract_InqValidationIdentifyNumber))
                     .ForMember(DtoC => DtoC.CvilCon_InqValMobile, opt => opt.MapFrom(C => C.CivilContract_InqValidationMobile))
                     .ForMember(DtoC => DtoC.CvilCon_InqValDesc, opt => opt.MapFrom(C => C.CivilContract_InqValidationDesc))
                     .ForMember(DtoC => DtoC.CvilCon_Amunt, opt => opt.MapFrom(C => C.CivilContract_Amount))
                     .ForMember(DtoC => DtoC.CvilCon_PreAmont, opt => opt.MapFrom(C => C.CivilContract_PreAmount))
                     .ForMember(DtoC => DtoC.CvilCon_Date, opt => opt.MapFrom(C => C.CivilContract_Date))
                     .ForMember(DtoC => DtoC.CvilCon_Desc, opt => opt.MapFrom(C => C.CivilContract_Desc))
                     .ForMember(DtoC => DtoC.CvilCon_DocName, opt => opt.MapFrom(C => C.CivilContract_DocumentName))
                     .ForMember(DtoC => DtoC.CvilCon_InqDocModiID, opt => opt.MapFrom(C => C.CivilContract_InqDocumentModifirID))
                     .ForMember(DtoC => DtoC.CvilCon_InqDocModiDate, opt => opt.MapFrom(C => C.CivilContract_InqDocumentModifirDate))
                     .ForMember(DtoC => DtoC.CvilCon_InqPolModiID, opt => opt.MapFrom(C => C.CivilContract_InqPoliceModifirID))
                     .ForMember(DtoC => DtoC.CvilCon_InqPolModiDate, opt => opt.MapFrom(C => C.CivilContract_InqPoliceModifirDate))
                     .ForMember(DtoC => DtoC.CvilCon_InqValModiID, opt => opt.MapFrom(C => C.CivilContract_InqValidationModifirID))
                     .ForMember(DtoC => DtoC.CvilCon_InqValModiDate, opt => opt.MapFrom(C => C.CivilContract_InqValidationModifirDate))
                     .ForMember(DtoC => DtoC.CvilCon_IsOwnerCar, opt => opt.MapFrom(C => C.CivilContract_IsOwnerCar))
                     .ForMember(DtoC => DtoC.CvilCon_IsOwnerCarDesc, opt => opt.MapFrom(C => C.CivilContract_IsOwnerCarDesc))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}
