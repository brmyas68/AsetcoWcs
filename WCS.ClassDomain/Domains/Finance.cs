

namespace WCS.ClassDomain.Domains
{
    public class Finance
    {
        public int Finance_ID { get; set; }
        public int Finance_WornMasterID { get; set; }
        public DateTime? Finance_RegisterDate { get; set; }
        public Int16 Finance_PaymentType { get; set; }
        public Int64? Finance_Amount { get; set; }
        public string Finance_Desc { get; set; }
        public int? Finance_ModifirID { get; set; }
        public DateTime? Finance_ModifirDate { get; set; }
    }
}
