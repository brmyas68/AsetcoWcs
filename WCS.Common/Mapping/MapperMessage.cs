


using AutoMapper;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;

namespace WCS.Common.Mapping
{
    public class MapperMessage
    {

        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(B =>
            {
                B.CreateMap<Message, DtoMessage>()
                      .ForMember(DtoM => DtoM.Msg_ID, opt => opt.MapFrom(M => M.Message_ID))
                      .ForMember(DtoM => DtoM.Msg_Mobile, opt => opt.MapFrom(M => M.Message_Mobile))
                      .ForMember(DtoM => DtoM.Msg_FullName, opt => opt.MapFrom(M => M.Message_FullName))
                      .ForMember(DtoM => DtoM.Msg_DateSend, opt => opt.MapFrom(M => M.Message_DateSend))
                      .ForMember(DtoM => DtoM.Msg_Text, opt => opt.MapFrom(M => M.Message_Text))
                      .ForMember(DtoM => DtoM.Msg_TenatID, opt => opt.MapFrom(M => M.Message_TenantID))
                      .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}

