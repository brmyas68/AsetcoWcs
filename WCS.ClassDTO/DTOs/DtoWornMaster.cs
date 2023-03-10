

namespace WCS.ClassDTO.DTOs
{
    public class DtoWornMaster
    {
        public int WMstr_ID { get; set; }
        public int WMstr_OwnID { get; set; }
        public int WMstr_WCarID { get; set; }
        public bool WMstr_WCarState { get; set; } 
        public int WMstr_AgtID { get; set; }
        public int WMstr_CvilConID { get; set; }
        public int WMstr_BusiID { get; set; }
        public DateTime WMstr_RegDate { get; set; }
        public string WMstr_TrckCode { get; set; }
        public Int16 WMstr_State { get; set; }
        public string WMstr_StateDesc { get; set; }
    }
}
