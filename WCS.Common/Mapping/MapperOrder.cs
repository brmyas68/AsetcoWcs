

using AutoMapper;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;

namespace WCS.Common.Mapping
{

    public class MapperOrder
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(B =>
            {
                B.CreateMap<Order, DtoOrder>()
                      .ForMember(DtoOP => DtoOP.Ordr_ID, opt => opt.MapFrom(OP => OP.Order_ID))
                      .ForMember(DtoOP => DtoOP.Ordr_UserID, opt => opt.MapFrom(OP => OP.Order_UserID))
                      .ForMember(DtoOP => DtoOP.Ordr_PID, opt => opt.MapFrom(OP => OP.Order_ProductID))
                      .ForMember(DtoOP => DtoOP.Ordr_DateReg, opt => opt.MapFrom(OP => OP.Order_DateRegister))
                      .ForMember(DtoOP => DtoOP.Ordr_Desc, opt => opt.MapFrom(OP => OP.Order_Desc))
                      .ForMember(DtoOP => DtoOP.Ordr_Count, opt => opt.MapFrom(OP => OP.Order_Count))
                      .ForMember(DtoOP => DtoOP.Ordr_ResComent, opt => opt.MapFrom(OP => OP.Order_ResultComment))
                      .ForMember(DtoOP => DtoOP.Ordr_Price, opt => opt.MapFrom(OP => OP.Order_Price))
                      .ForMember(DtoOP => DtoOP.Ordr_OrdrCod, opt => opt.MapFrom(OP => OP.Order_OrderCode))
                      .ForMember(DtoOP => DtoOP.Ordr_PType, opt => opt.MapFrom(OP => OP.Order_PType))
                      .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}
