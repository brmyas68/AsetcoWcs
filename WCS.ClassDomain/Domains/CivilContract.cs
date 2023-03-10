

namespace WCS.ClassDomain.Domains
{
    public class CivilContract
    {
        public int CivilContract_ID { get; set; }
        public bool? CivilContract_InqDocumentState { get; set; }
        public string CivilContract_InqDocumentDesc { get; set; }
        public Int64? CivilContract_InqViolationAmount { get; set; }
        public bool? CivilContract_InqBlockState { get; set; }
        public bool? CivilContract_InqViolationState { get; set; }
        public string CivilContract_InqPoliceDesc { get; set; }
        public bool? CivilContract_InqValidationIdentifyNumber { get; set; }
        public bool? CivilContract_InqValidationMobile { get; set; }
        public string CivilContract_InqValidationDesc { get; set; }
        public Int64? CivilContract_Amount { get; set; }
        public Int64? CivilContract_PreAmount { get; set; }
        public DateTime? CivilContract_Date { get; set; }
        public string CivilContract_Desc { get; set; }
        public string CivilContract_DocumentName { get; set; }
        public int? CivilContract_InqDocumentModifirID { get; set; }
        public DateTime? CivilContract_InqDocumentModifirDate { get; set; }
        public int? CivilContract_InqPoliceModifirID { get; set; }
        public DateTime? CivilContract_InqPoliceModifirDate { get; set; }
        public int? CivilContract_InqValidationModifirID { get; set; }
        public DateTime? CivilContract_InqValidationModifirDate { get; set; }
        public bool? CivilContract_IsOwnerCar { get; set; }
        public string CivilContract_IsOwnerCarDesc { get; set; }

    }
}
