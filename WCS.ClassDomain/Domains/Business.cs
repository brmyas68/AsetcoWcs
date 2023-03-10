

namespace WCS.ClassDomain.Domains
{
    public class Business
    {
        public int Business_ID { get; set; }
        public Int64? Business_MinPrice { get; set; }
        public Int64? Business_MaxPrice { get; set; }
        public string Business_PriceDesc { get; set; }
        public DateTime? Business_SendDate { get; set; }
        public int? Business_SplitWornCenterID { get; set; }
        public int? Business_ParkingWornCenterID { get; set; }
        public DateTime? Business_SplitDate { get; set; }
        public string Business_SplitDesc { get; set; }
        public DateTime? Business_ParkingDate { get; set; }
        public string Business_ParkingDesc { get; set; }
        public Int64? Business_AgreementAmount { get; set; }
        public Int64? Business_PreAgreementAmount { get; set; }
        public int? Business_Investor { get; set; }
        public string Business_AgreementDesc { get; set; }
    }
}
