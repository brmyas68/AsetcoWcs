

namespace WCS.ClassDomain.Domains
{
    public class WornCars
    {
        public int WornCars_ID { get; set; }
     //   public int WornCars_BrandID { get; set; }
        public int WornCars_ModelID { get; set; }
        public Int16 WornCars_UserType { get; set; }
        public Int16 WornCars_PelakType { get; set; }
        public int? WornCars_BuildYear { get; set; }
        public Int16 WornCars_State { get; set; }
        public bool WornCars_SellType { get; set; }

        public int? WornCars_Weight { get; set; }
        public Int16? WornCars_DocumentType { get; set; }
        public string WornCars_Desc { get; set; }

    }
}
