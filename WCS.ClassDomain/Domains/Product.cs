
namespace WCS.ClassDomain.Domains
{
    public class Product
    {
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public string Product_NameEn { get; set; }
        public string Product_Model { get; set; }
        public string Product_Series { get; set; }
        public Int64 Product_Price { get; set; }
        public Int16 Product_Group { get; set; }
        public Int16 Product_Type { get; set; }
        public bool Product_IsUsed { get; set; }
        public string Product_Description { get; set; }
        public DateTime? Product_RegisterDate { get; set; }

    }
}
