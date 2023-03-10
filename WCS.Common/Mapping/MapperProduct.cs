using AutoMapper;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;

namespace WCS.Common.Mapping
{
    public class MapperProduct
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(P =>
            {
                P.CreateMap<Product, DtoProduct>()
                    .ForMember(DtoP => DtoP.Pro_ID, opt => opt.MapFrom(P => P.Product_ID))
                    .ForMember(DtoP => DtoP.Prod_Name, opt => opt.MapFrom(P => P.Product_Name))
                    .ForMember(DtoP => DtoP.Prod_NameEn, opt => opt.MapFrom(P => P.Product_NameEn))
                    .ForMember(DtoP => DtoP.Prod_Model, opt => opt.MapFrom(P => P.Product_Model))
                    .ForMember(DtoP => DtoP.Prod_Serie, opt => opt.MapFrom(P => P.Product_Series))
                    .ForMember(DtoP => DtoP.Prod_Group, opt => opt.MapFrom(P => P.Product_Group))
                    .ForMember(DtoP => DtoP.Prod_IsUsed, opt => opt.MapFrom(P => P.Product_IsUsed))
                    .ForMember(DtoP => DtoP.Prod_Price, opt => opt.MapFrom(P => P.Product_Price))
                    .ForMember(DtoP => DtoP.Prod_Desc, opt => opt.MapFrom(P => P.Product_Description))
                    .ForMember(DtoP => DtoP.Prod_Type, opt => opt.MapFrom(P => P.Product_Type))
                    .ForMember(DtoP => DtoP.Prod_RegDate, opt => opt.MapFrom(P => P.Product_RegisterDate))
                    .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}