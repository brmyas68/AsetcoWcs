

namespace WCS.ClassDTO.DTOs
{
    public class DtoMessage
    {
        public int Msg_ID { get; set; }
        public string Msg_FullName { get; set; }
        public string Msg_Mobile { get; set; }
        public string Msg_Text { get; set; }
        public int Msg_TenatID { get; set; } 
        public DateTime? Msg_DateSend { get; set; }
    }
}

