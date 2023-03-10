

namespace WCS.ClassDomain.Domains
{
    public class Order
    {
        public int Order_ID { get; set; }
        public int Order_UserID { get; set; }
        public int Order_ProductID { get; set; }
        public DateTime Order_DateRegister { get; set; }
        public string Order_Desc { get; set; }
        public int Order_Count { get; set; }
        public Int64 Order_Price { get; set; }
        public Int16? Order_ResultComment { get; set; }
        public int? Order_OrderCode { get; set; }
        public Int16 Order_PType { get; set; }

    }
}
