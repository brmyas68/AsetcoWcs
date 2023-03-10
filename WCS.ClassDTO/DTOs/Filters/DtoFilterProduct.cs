using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCS.ClassDTO.DTOs.Filters
{
    public class DtoFilterProduct
    {
        public List<string> Filter_ProductGroup { get; set; }
        public string Filter_ProductsType { get; set; }
        public List<string> Filter_ProductsIsUsed { get; set; }
    }
}
