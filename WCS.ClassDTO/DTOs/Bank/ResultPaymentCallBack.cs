

namespace WCS.ClassDTO.DTOs.Bank
{
    public class ResultPaymentCallBack
    {
        public Int64 Token { get; set; }
        public Int64 OrderId { get; set; }
        public Int32 TerminalNo { get; set; }
        public Int64 RRN { get; set; }
        public Int16 status { get; set; }
        public String HashCardNumber { get; set; }
        public Int64 Amount { get; set; }
        public String TspToken { get; set; }
    }
}
