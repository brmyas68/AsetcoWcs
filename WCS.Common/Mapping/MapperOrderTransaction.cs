

using AutoMapper;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;

namespace WCS.Common.Mapping
{

    public class MapperOrderTransaction
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(O =>
            {
                O.CreateMap<OrderTransaction, DtoOrderTransaction>()
                      .ForMember(DtoOP => DtoOP.OrdTrans_ID, opt => opt.MapFrom(OP => OP.OrderTransaction_ID))
                      .ForMember(DtoOP => DtoOP.OrdTrans_UsrID, opt => opt.MapFrom(OP => OP.OrderTransaction_UserID))
                      .ForMember(DtoOP => DtoOP.OrdTrans_DateTrans, opt => opt.MapFrom(OP => OP.OrderTransaction_DateTrans))
                      .ForMember(DtoOP => DtoOP.OrdTrans_TrackCode, opt => opt.MapFrom(OP => OP.OrderTransaction_TrackingCode))
                      .ForMember(DtoOP => DtoOP.OrdTrans_Price, opt => opt.MapFrom(OP => OP.OrderTransaction_Price))
                      .ForMember(DtoOP => DtoOP.OrdTrans_OrdrCod, opt => opt.MapFrom(OP => OP.OrderTransaction_OrderCode))
                      .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}
