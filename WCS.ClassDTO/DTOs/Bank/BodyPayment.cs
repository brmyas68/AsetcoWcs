using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCS.ClassDTO.DTOs.Bank
{

    public class BodyPayment
    {
        public Int64 Amount { get; set; }
        public Int64 OrderId { get; set; }
        public String CallBackUrl { get; set; }
        public String AdditionalData { get; set; }
        public String Originator { get; set; }
    }
}
