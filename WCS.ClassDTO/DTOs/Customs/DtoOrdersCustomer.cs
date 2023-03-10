using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCS.ClassDTO.DTOs.Customs
{
    public class DtoOrdersCustomer
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public int City { get; set; }
        public int Province { get; set; }
        public string Address { get; set; }
        public string Tell { get; set; }
        public List<DtoOrdersProduct> OrdersProducts { get; set; }
    }

    public class DtoOrdersProduct
    {
        public int PID { get; set; }
        public int PCount { get; set; }
        public Int16 ResltComnt { get; set; }
    }
}
