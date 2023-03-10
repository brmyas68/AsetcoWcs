using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCS.Common.StoredProcedure
{
    public static class SPWCS
    {
        public enum SPName
        {
            UC_CheckPermissions,
            Wcs_GetAllWornCars,
            Wcs_GetAll, Wcs_GetByWornMaster,
            Wcs_GetAllOrders, Wcs_GetByOrder,
            Wcs_DetailListBuyProducts,
            Wcs_GetAllProduct,
            Wcs_GetProductPriceById, Wcs_GetAllProductPrice,
        };

    }
}
