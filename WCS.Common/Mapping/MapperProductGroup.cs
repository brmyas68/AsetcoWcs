using AutoMapper;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;

namespace WCS.Common.Mapping
{
    public class MapperProductGroup
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(PG =>
            {
                PG.CreateMap<ProductGroup, DtoProductGroup>()
                      .ForMember(DtoPG => DtoPG.PG_ID, opt => opt.MapFrom(PG => PG.ProductGroup_ID))
                      .ForMember(DtoPG => DtoPG.PG_Name, opt => opt.MapFrom(PG => PG.ProductGroup_Name))
                      .ForMember(DtoPG => DtoPG.PG_Type, opt => opt.MapFrom(PG => PG.ProductGroup_Type))
                      .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}
