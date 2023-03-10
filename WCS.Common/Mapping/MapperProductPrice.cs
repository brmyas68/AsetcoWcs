
using AutoMapper;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;

namespace WCS.Common.Mapping
{
    public class MapperProductPrice
    {
        public static IMapper MapTo()
        {
            var MappingConfig = new MapperConfiguration(P =>
            {
                P.CreateMap<ProductPrice, DtoProductPrice>()
                    .ForMember(DtoP => DtoP.ProPric_ID, opt => opt.MapFrom(P => P.ProductPrice_ID))
                    .ForMember(DtoP => DtoP.ProPric_PId, opt => opt.MapFrom(P => P.ProductPrice_ProductId))
                    .ForMember(DtoP => DtoP.ProPric_Price, opt => opt.MapFrom(P => P.ProductPrice_Price))
                    .ForMember(DtoP => DtoP.ProPric_Date, opt => opt.MapFrom(P => P.ProductPrice_Date))
                    .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}

