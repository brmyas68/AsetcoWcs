

namespace WCS.ClassDTO.DTOs
{
    public class DtoOrder
    {
        public int Ordr_ID { get; set; }
        public int Ordr_UserID { get; set; }
        public int Ordr_PID { get; set; }
        public DateTime Ordr_DateReg { get; set; }
        public string Ordr_Desc { get; set; }
        public Int16? Ordr_ResComent { get; set; }
        public int Ordr_Count { get; set; }
        public Int64 Ordr_Price { get; set; }
        public int? Ordr_OrdrCod { get; set; }
        public Int16 Ordr_PType { get; set; }
    }
}
