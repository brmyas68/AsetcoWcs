

namespace WCS.ClassDTO.DTOs
{
    public class DtoCivilContract
    {
        public int CvilCon_ID { get; set; }
        public bool? CvilCon_InqDocState { get; set; }

        public string CvilCon_InqDocDesc { get; set; }
        public Int64? CvilCon_InqViolAmont { get; set; }
        public bool? CvilCon_InqViolState { get; set; }
        public bool? CvilCon_InqBlkState { get; set; }
        public string CvilCon_InqPolDesc { get; set; }
        public bool? CvilCon_InqValIdentifyNumber { get; set; }
        public bool? CvilCon_InqValMobile { get; set; }
        public string CvilCon_InqValDesc { get; set; }
        public Int64? CvilCon_Amunt { get; set; }
        public Int64? CvilCon_PreAmont { get; set; }
        public DateTime? CvilCon_Date { get; set; }
        public string CvilCon_Desc { get; set; }
        public string CvilCon_DocName { get; set; }
        public int? CvilCon_InqDocModiID { get; set; }
        public DateTime? CvilCon_InqDocModiDate { get; set; }
        public int? CvilCon_InqPolModiID { get; set; }
        public DateTime? CvilCon_InqPolModiDate { get; set; }
        public int? CvilCon_InqValModiID { get; set; }
        public DateTime? CvilCon_InqValModiDate { get; set; }
        public bool? CvilCon_IsOwnerCar { get; set; }
        public string CvilCon_IsOwnerCarDesc { get; set; }

    }
}
