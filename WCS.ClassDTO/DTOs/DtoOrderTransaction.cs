using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCS.ClassDTO.DTOs
{
    public class DtoOrderTransaction
    {
        public int OrdTrans_ID { get; set; }
        public int OrdTrans_UsrID { get; set; }
        public DateTime OrdTrans_DateTrans { get; set; }
        public string OrdTrans_TrackCode { get; set; }
        public Int64 OrdTrans_Price { get; set; }
        public int? OrdTrans_OrdrCod { get; set; }
    }
}
