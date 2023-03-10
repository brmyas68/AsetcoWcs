

namespace WCS.ClassDTO.DTOs
{
    public class DtoWornCars
    {
        public int WCars_ID { get; set; }
      //  public int WCars_BrdID { get; set; }
        public int WCars_ModID { get; set; }
        public Int16 WCars_UsrType { get; set; }
        public Int16 WCars_PlkType { get; set; }
        public int WCars_BldYear { get; set; }
        public Int16 WCars_State { get; set; }
        public bool WCars_SellType { get; set; }

        public int WCars_Weight { get; set; }
        public Int16 WCars_DocType { get; set; } 
        public string WCars_Desc { get; set; }

    }
}
