

using AutoMapper;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;

namespace WCS.Common.Mapping
{
    public class MapperBusiness
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(B =>
            {
                B.CreateMap<Business, DtoBusiness>()
                      .ForMember(DtoB => DtoB.Busi_ID, opt => opt.MapFrom(B => B.Business_ID))
                      .ForMember(DtoB => DtoB.Busi_SplitWCntrID, opt => opt.MapFrom(B => B.Business_SplitWornCenterID))
                      .ForMember(DtoB => DtoB.Busi_MinPrice, opt => opt.MapFrom(B => B.Business_MinPrice))
                      .ForMember(DtoB => DtoB.Busi_MaxPrice, opt => opt.MapFrom(B => B.Business_MaxPrice))
                       .ForMember(DtoB => DtoB.Busi_PriceDesc, opt => opt.MapFrom(B => B.Business_PriceDesc))
                      .ForMember(DtoB => DtoB.Busi_SendDate, opt => opt.MapFrom(B => B.Business_SendDate))
                      .ForMember(DtoB => DtoB.Busi_ParkWCntrID, opt => opt.MapFrom(B => B.Business_ParkingWornCenterID))
                      .ForMember(DtoB => DtoB.Busi_SplitDate, opt => opt.MapFrom(B => B.Business_SplitDate))
                      .ForMember(DtoB => DtoB.Busi_SplitDesc, opt => opt.MapFrom(B => B.Business_SplitDesc))
                      .ForMember(DtoB => DtoB.Busi_ParkDate, opt => opt.MapFrom(B => B.Business_ParkingDate))
                      .ForMember(DtoB => DtoB.Busi_ParkDesc, opt => opt.MapFrom(B => B.Business_ParkingDesc))
                      .ForMember(DtoB => DtoB.Busi_AgrmentAmont, opt => opt.MapFrom(B => B.Business_AgreementAmount))
                      .ForMember(DtoB => DtoB.Busi_PreAgrmentAmont, opt => opt.MapFrom(B => B.Business_PreAgreementAmount))
                      .ForMember(DtoB => DtoB.Busi_AgrmentDesc, opt => opt.MapFrom(B => B.Business_AgreementDesc))
                      .ForMember(DtoB => DtoB.Busi_Investor, opt => opt.MapFrom(B => B.Business_Investor))
                      .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}
