

namespace WCS.ClassDTO.DTOs
{
    public class DtoBusiness
    {
        public int Busi_ID { get; set; }
        public Int64 Busi_MinPrice { get; set; }
        public Int64 Busi_MaxPrice { get; set; }
        public string Busi_PriceDesc { get; set; }
        public DateTime? Busi_SendDate { get; set; }
        public int Busi_SplitWCntrID { get; set; }
        public int Busi_ParkWCntrID { get; set; }
        public DateTime? Busi_SplitDate { get; set; }
        public string Busi_SplitDesc { get; set; }
        public DateTime? Busi_ParkDate { get; set; }
        public string Busi_ParkDesc { get; set; }
        public Int64 Busi_AgrmentAmont { get; set; }
        public Int64 Busi_PreAgrmentAmont { get; set; }
        public int Busi_Investor { get; set; }
        public string Busi_AgrmentDesc { get; set; }

    }
}
