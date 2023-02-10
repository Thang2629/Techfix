using Managerment.OrderWarranties;
using Managerment.ProductRepairs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Managerment.ProductWarranties
{
    public class ProductWarrantyDto
    {
        public string PW_code { get; set; }
        public string PW_name { get; set; }
        public DateTime PW_date_finish { get; set; }
        public string PW_status { get; set; }
        public EProductWarrantyType Product_warranty_type { get; set; }
        public int Warranty_times { get; set; }
        public string Fix_note { get; set; }
        public DetailProductRepairCreateOrderDetailDto Detail_product_pepairs { get; set; }
    }
}
