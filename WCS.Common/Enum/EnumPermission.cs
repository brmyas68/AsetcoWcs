

namespace WCS.Common.Enum
{
    public static class EnumPermission
    {
        public enum Role
        {
            Role_WCS_User_WornCar = 25, // GET Role by this  tag id
            Role_WCS_User_Investor = 27,
            Role_WCS_Agent_WornCar = 26,
        };

        public enum Controllers
        {
            Form_WCS_Product,
            Form_WCS_ProductGroup,
            Form_WCS_ProductPrice,
            Form_WCS_Order,
            Form_WCS_OrderTransaction,
            Form_WCS_WornMaster,
            Form_WCS_Message,
            Form_WCS_Finance,
            Form_WCS_Civil,
            Form_WCS_Business,
        };

        public enum Actions
        {
            Action_WCS_Product_GetAll,
            Action_WCS_Product_Insert,
            Action_WCS_Product_Update,
            Action_WCS_Product_Delete,
            Action_WCS_Product_BuyDetailProducts,
            Action_WCS_Product_UploadFile,
            Action_WCS_Product_DeleteFile,

            Action_WCS_ProductGroup_GetAll,
            Action_WCS_ProductGroup_Insert,
            Action_WCS_ProductGroup_Update,
            Action_WCS_ProductGroup_Delete,
            Action_WCS_ProductGroup_GetByID,

            Action_WCS_ProductPrice_GetAll,
            Action_WCS_ProductPrice_Insert,
            Action_WCS_ProductPrice_Update,
            Action_WCS_ProductPrice_Delete,
            Action_WCS_ProductPrice_GetByID,

            Action_WCS_WornMaster_GetAll,
            Action_WCS_WornMaster_GetByID,
            Action_WCS_WornMaster_InquiryPrice,
            Action_WCS_WornMaster_Insert,
            Action_WCS_WornMaster_DeleteFile,
            Action_WCS_WornMaster_UploadImage,

            Action_WCS_Order_GetAll,
            Action_WCS_Order_GetAllByUser,
            Action_WCS_Order_GetByID,

            Action_WCS_OrderTransaction_GetAll,
            Action_WCS_OrderTransaction_GetAllByUser,
            Action_WCS_OrderTransaction_GetByID,
            Action_WCS_OrderTransaction_GetByOrderCode,

            Action_WCS_Message_GetAll,
            Action_WCS_Message_Delete,

            Action_WCS_Finance_DeleteFile,
            Action_WCS_Finance_GetAllFiles,
            Action_WCS_Finance_UploadFile,
            Action_WCS_Finance_Delete,
            Action_WCS_Finance_Update,
            Action_WCS_Finance_Insert,
            Action_WCS_Finance_GetAll,

            Action_WCS_Civil_InquiryCivilRegister,
            Action_WCS_Civil_InquirypoliceTraffic,
            Action_WCS_Civil_InquiryOffice,
            Action_WCS_Civil_LoadingContracts,
            Action_WCS_Civil_DeleteFile,

            Action_WCS_Business_DeterminSuggestPrice,
            Action_WCS_Business_AgreedPrice,
            Action_WCS_Business_DeterminParking,
            Action_WCS_Business_DeterminWornCenter,
            Action_WCS_Business_StateTrackingCode,
            Action_WCS_Business_GetAllInvestor,
        }

    }
}
