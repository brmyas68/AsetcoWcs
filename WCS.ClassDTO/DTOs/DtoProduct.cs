
namespace WCS.ClassDTO.DTOs
{
    public class DtoProduct
    {
        public int Pro_ID { get; set; }
        public string Prod_Name { get; set; }
        public string Prod_NameEn { get; set; }
        public string Prod_Model { get; set; }
        public string Prod_Serie { get; set; }
        public Int64 Prod_Price { get; set; }
        public Int16 Prod_Group { get; set; }
        public Int16 Prod_Type { get; set; }
        public bool Prod_IsUsed { get; set; }
        public string Prod_Desc { get; set; }
        public string Prod_Image { get; set; }
        public DateTime? Prod_RegDate { get; set; }

    }
}
