

namespace WCS.ClassDTO.DTOs.Bank
{
    public class ResultConfirm
    {
        public Int16 Status { get; set; }
        public Int64 Token { get; set; }
        public Int64 RRN { get; set; }
        public String CardNumberMasked { get; set; }
    }
}
