using System;
using System.Collections.Generic;
using System.Text;

namespace Managerment.OrderRepairs
{
    public class OrderRepairByCustomerDto
    {
        public string Cus_name { get; set; }
        public string Order_repair_code { get; set; }
        public int Total_count { get; set; }
        public decimal Total_money { get; set; }
    }
}
