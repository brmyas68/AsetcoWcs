

namespace WCS.ClassDomain.Domains
{
    public class WornMaster
    {
        public int WornMaster_ID { get; set; }
        public int WornMaster_OwnerID { get; set; }
        public int WornMaster_WornCarID { get; set; }
        public bool WornMaster_WornCarState { get; set; } 
        public int? WornMaster_AgentID { get; set; }
        public int? WornMaster_CivilContractID { get; set; }
        public int? WornMaster_BusinessID { get; set; }
        public DateTime? WornMaster_RegisterDate { get; set; }
        public string WornMaster_TrackingCode { get; set; }
        public Int16 WornMaster_State { get; set; }
        public string WornMaster_StateDesc { get; set; }
    }
}
