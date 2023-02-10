using System;
using System.Collections.Generic;
using System.Text;

namespace Managerment.OrderRepairSales
{
    public class OrderRepairSaleDto
    {
        public string Order_repair_code { get; set; }
        public string Cus_name { get; set; }
        public string Store_name { get; set; }
        public DateTime Export_date { get; set; }
        public string User_name { get; set; }
        public int Total_count { get; set; }
        public decimal Sales_price { get; set; }
    }
}
