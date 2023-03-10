 

namespace WCS.ClassDTO.DTOs
{
    public class DtoFinance
    {
        public int Fin_ID { get; set; }
        public int Fin_WrnMastrID { get; set; }
        public DateTime Fin_RegDate { get; set; }
        public Int16 Fin_PayType { get; set; }
        public Int64 Fin_Amont { get; set; }
        public string Fin_Desc { get; set; }
        public int Fin_ModiID { get; set; }
        public DateTime Fin_ModiDate { get; set; }

    }
}
