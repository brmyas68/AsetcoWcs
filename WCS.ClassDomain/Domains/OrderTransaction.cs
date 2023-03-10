using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCS.ClassDomain.Domains
{
    public class OrderTransaction
    {
        public int OrderTransaction_ID { get; set; }
        public int OrderTransaction_UserID { get; set; }
        public DateTime OrderTransaction_DateTrans { get; set; }
        public string OrderTransaction_TrackingCode { get; set; }
        public Int64 OrderTransaction_Price { get; set; }
        public int? OrderTransaction_OrderCode { get; set; }

    }
}
