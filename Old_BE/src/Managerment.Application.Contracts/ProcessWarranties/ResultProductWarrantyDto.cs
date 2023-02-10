using System;
using System.Collections.Generic;
using System.Text;

namespace Managerment.ProcessWarranties
{
    public class ResultProductWarrantyDto
    {
        public int Warranty_count { get; set; }
        public int Warranty_done { get; set; }
        public int Warranty_return_customer { get; set; }
        public decimal Total_warranty_cost { get; set; }
        public List<ProcessWarrantyDto> Warranty_lst { get; set; }
    }
}
